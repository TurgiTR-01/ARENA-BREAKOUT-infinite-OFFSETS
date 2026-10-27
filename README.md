# ARENA-BREAKOUT-infinite-OFFSETS
OFFSETS ARENA BREAKOUT İNFİNİTE PC 
BİLGİLENDİRME

They added encryptions.

Arena Breakout: Infinite ACE Anti-Cheat Position Encryption — Full Reverse Engineering

Overview

As of the June 2026 patch, ACE (Anti-Cheat Expert) encrypts all position-related fields on USceneComponent — RelativeLocation, ComponentToWorld, and similar transforms. Reading these fields directly now returns garbage. This post documents the full encryption scheme, cache structure, and decryption algorithm reversed from the game binary.

ACE uses a per-component XOR stream cipher backed by a global hash table cache. Each encrypted field has a 4-byte control DWORD stored at a fixed offset after the field. The top 3 bits select the algorithm (0 = plaintext, nonzero = encrypted), and the lower 25 bits are a cache key used to look up the XOR seed and encrypted data.


Control DWORD Layout

Bits 31-29: Algorithm (0 = plaintext, nonzero = encrypted)
Bits 24-0: Cache key (25-bit)

Key offsets on USceneComponent:
- RelativeLocation control DWORD: component + 0x17C (field data at 0x170)
- ComponentToWorld control DWORD: component + 0x250 (FTransform at 0x220)

When algo == 0, the field is unencrypted — just read the floats directly. A sentinel value of 0xFF8B6D2B at the field offset indicates a dead/destroyed actor.


Cache Table

ACE maintains a global hash table at a fixed RVA. In the June 2026 build:

Cache table RVA: 0xAF8A390 (from game base)
Table size: 0x10001 entries (65537)

Each entry is a linked list node:

+0x00 (4 bytes) - type
+0x10 (8 bytes) - data_ptr (pointer to encrypted DWORD array)
+0x18 (4 bytes) - data_size (byte count of encrypted data)
+0x1C (4 bytes) - key (matches the 25-bit cache key)
+0x20 (4 bytes) - seed (per-entry RNG seed for XOR generation)
+0x28 (8 bytes) - next_ptr (next node in chain, 0 = end)

Hash function to find the bucket:

bucket = (0x9E3779B1 * key) % 0x10001
entry = read_uint64(game_base + CACHE_RVA + bucket * 8)

Walk the linked list at entry, comparing node.key against your 25-bit cache key until you find a match.


XOR Mask Generation

The cipher generates a 4-byte XOR mask per DWORD using a murmur-style hash with TEA-derived constants. The mask is applied sequentially to each DWORD of encrypted data.

Constants (all derived from TEA's DELTA = 0x9E3779B9):
STEP = 0x78DDE6E4 (4 * DELTA)
v16_init = 0x3C6EF372 (2 * DELTA)
v17_init = 0xDAA66D2B (3 * DELTA, unsigned)

Murmur hash helper:

uint32 ace_murmur(uint32 x) {
return 73244475 * (x ^ (x >> 16));
}

Mask function — produces one 4-byte XOR mask from the running state:

uint32 ace_mask(uint32 v12, uint32 seed, uint32 v16, uint32 v17) {
uint32 sa = ace_murmur(seed ^ v12 ^ (v17 + 626627285));
uint32 sb = ace_murmur(seed ^ v12 ^ (v17 - 1013904242));
uint32 sc = ace_murmur(seed ^ v12 ^ v16);

uint32 w = seed ^ v12 ^ v17;
w ^= (w >> 16);
uint32 sd = (73244475 * w) ^ (2671443968 * w);

return ((sa ^ (sa >> 16)) & 0xFF)
| (((sb << 8) ^ (sb >> 8)) & 0xFF00)
| ((sc ^ (sc << 16)) & 0xFF0000)
| ((sd << 8) & 0xFF000000);
}

Each byte of the mask comes from a different sub-hash — this is NOT a standard murmur or TEA, it's ACE's custom construction.


Decryption — RelativeLocation (Vec3)

RelativeLocation is 3 encrypted DWORDs (12 bytes). The control DWORD is at component + 0x17C.

Vector3 decrypt_relative_location(uint64 component) {
uint32 ctl = read_uint32(component + 0x17C);
uint32 algo = ctl >> 29;
uint32 key = ctl & 0x1FFFFFF;

// Plaintext path
if (algo == 0) {
if (read_uint32(component + 0x170) == 0xFF8B6D2B)
return {0, 0, 0}; // dead actor sentinel
return read_vector3(component + 0x170);
}
if (key == 0) return {0, 0, 0};

// Cache lookup
uint32 bucket = (0x9E3779B1 * key) % 0x10001;
uint64 entry = read_uint64(game_base + 0xAF8A390 + bucket * 8);
while (entry) {
if (read_uint32(entry + 0x1C) == key) break;
entry = read_uint64(entry + 0x28);
}
if (!entry) return {0, 0, 0};

uint64 data_ptr = read_uint64(entry + 0x10);
uint32 data_size = read_uint32(entry + 0x18);
uint32 seed = read_uint32(entry + 0x20);

if (data_size < 12) return {0, 0, 0};

// Read encrypted DWORDs
uint32 d0 = read_uint32(data_ptr + 0);
uint32 d1 = read_uint32(data_ptr + 4);
uint32 d2 = read_uint32(data_ptr + 8);

// XOR stream
uint32 v12 = 0x9E3779B1 * key;
uint32 v16 = 0x3C6EF372; // 2 * DELTA
uint32 v17 = 0xDAA66D2B; // 3 * DELTA

d0 ^= ace_mask(v12, seed, v16, v17);
v16 += 0x78DDE6E4; v17 += 0x78DDE6E4; // step = 4 * DELTA
d1 ^= ace_mask(v12, seed, v16, v17);
v16 += 0x78DDE6E4; v17 += 0x78DDE6E4;
d2 ^= ace_mask(v12, seed, v16, v17);

return { bit_cast_float(d0), bit_cast_float(d1), bit_cast_float(d2) };
}


Decryption — ComponentToWorld Translation (FTransform)

ComponentToWorld is a full 48-byte FTransform (Rotation Quat + Translation Vec3 + Scale Vec3). The control DWORD is at component + 0x250. To extract just the translation (bytes 16-27), skip the first 4 DWORD steps:

Vector3 decrypt_c2w_translation(uint64 component) {
uint32 ctl = read_uint32(component + 0x250);
uint32 algo = ctl >> 29;
uint32 key = ctl & 0x1FFFFFF;

if (algo == 0) {
// Plaintext — FTransform starts at 0x220, translation at +0x10
return read_vector3(component + 0x230);
}
if (key == 0) return {0, 0, 0};

// Cache lookup (same as above)
uint32 bucket = (0x9E3779B1 * key) % 0x10001;
uint64 entry = read_uint64(game_base + 0xAF8A390 + bucket * 8);
while (entry) {
if (read_uint32(entry + 0x1C) == key) break;
entry = read_uint64(entry + 0x28);
}
if (!entry) return {0, 0, 0};

uint64 data_ptr = read_uint64(entry + 0x10);
uint32 data_size = read_uint32(entry + 0x18);
uint32 seed = read_uint32(entry + 0x20);

if (data_size < 48) return {0, 0, 0};

// Translation DWORDs (indices 4, 5, 6 of the encrypted block)
uint32 d4 = read_uint32(data_ptr + 16);
uint32 d5 = read_uint32(data_ptr + 20);
uint32 d6 = read_uint32(data_ptr + 24);

uint32 v12 = 0x9E3779B1 * key;
// Skip 4 DWORD positions to reach translation
uint32 v16 = 0x3C6EF372 + 4 * 0x78DDE6E4;
uint32 v17 = 0xDAA66D2B + 4 * 0x78DDE6E4;

d4 ^= ace_mask(v12, seed, v16, v17);
v16 += 0x78DDE6E4; v17 += 0x78DDE6E4;
d5 ^= ace_mask(v12, seed, v16, v17);
v16 += 0x78DDE6E4; v17 += 0x78DDE6E4;
d6 ^= ace_mask(v12, seed, v16, v17);

return { bit_cast_float(d4), bit_cast_float(d5), bit_cast_float(d6) };
}


Notes

- The cache table pointer, control DWORD offsets, and all constants are from the June 2026 ABI build. These will shift with future patches — the algorithm itself is unlikely to change.
- v12 is derived solely from key, while v16/v17 are counters that advance by STEP per DWORD. seed is per-cache-entry (per-component). This means every component gets a unique XOR stream.
- The linked list walk should be capped (I use 32 iterations) to avoid infinite loops on corrupted cache entries.
- algo == 0 (top 3 bits clear) means the field was never encrypted or ACE hasn't activated yet for that component. Always handle this plaintext fallback.
- The dead actor sentinel 0xFF8B6D2B appears at the field offset (not the control DWORD) when the actor is destroyed. Check before reading floats.
- To decrypt the full FTransform (rotation + translation + scale), just run the XOR stream from DWORD index 0 through 11 (all 48 bytes / 12 DWORDs).

Reversed from sub_140BC6A40 in the game binary. The function is the fast-path decryptor called inline whenever the engine reads an encrypted property.
