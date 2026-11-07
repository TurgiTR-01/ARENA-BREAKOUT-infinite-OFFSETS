# ARENA-BREAKOUT-infinite-OFFSETS
OFFSETS ARENA BREAKOUT İNFİNİTE PC 
NEW OFFSETS
xor_key = read_u8(ImageBase + 0xBB1AFEC)   # Seeded dynamically per game launch via QueryPerformanceCounter
dl = ((xor_key >> 5) & 2) ^ xor_key
cl = ((dl & 2) << 5) ^ dl
mask_eff = (((cl >> 5) & 2) ^ cl ^ 0x65) & 0xFF
# Example 1: xor_key = 0x78 -> dl = 0x7A, cl = 0x3A -> mask_eff = 0x5F
# Example 2: xor_key = 0x55 -> dl = 0x57, cl = 0x17 -> mask_eff = 0x30
