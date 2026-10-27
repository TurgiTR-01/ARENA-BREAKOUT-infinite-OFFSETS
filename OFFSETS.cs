namespace Offsets {
    uint64 GObjects      = 0xA5B9EB8;
    uint64 GWorld        = 0xACB88A8;
    uint64 FNameKey      = 0xB49A94C;
    uint64 FNamePool     = 0xB4E2F40;
    uint64 GWorldDecrypt = 0xDF7CE0;
    uint64 FNameDecrypt  = 0x3BA80D0;
 
    namespace UWorld {
        uint64 PersistentLevel    = 0x0030;
        uint64 Levels             = 0x0138;
        uint64 OwningGameInstance = 0x0180;
        uint64 GameState          = 0x0120;
    }
 
    namespace ULevel {
        uint64 Actors      = 0x0098;
        uint64 ActorsCount = Actors + 0x8;
    }
 
    namespace UGameInstance {
        uint64 LocalPlayers = 0x0038;
    }
 
    namespace UPlayer {
        uint64 PlayerController = 0x0030;
    }
 
    namespace APlayerController {
        uint64 AcknowledgedPawn    = 0x0398;
        uint64 PlayerCameraManager = 0x03B0;
        uint64 ControlRotation     = 0x0380;
    }
 
    namespace APlayerCameraManager {
        uint64 CameraCachePrivate = 0x20E0;
    }
 
    namespace FCameraCacheEntry {
        uint64 FMinimalViewInfo = 0x0010;
    }
 
    namespace AActor {
        uint64 Owner         = 0x0118;
        uint64 Children      = 0x0160;
        uint64 RootComponent = 0x0170;
    }
 
    namespace USceneComponent {
        uint64 AttachParent     = 0x0108;
        uint64 RelativeLocation = 0x0170;
        uint64 ACE_RelLoc_Ctl   = 0x017C;
    }
 
    namespace UActorComponent {
        uint64 ComponentOwner = 0x0020;
    }
 
    namespace ACharacter {
        uint64 Mesh             = 0x0388;
        uint64 CapsuleComponent = 0x0398;
    }
 
    namespace UCapsuleComponent {
        uint64 CapsuleHalfHeight = 0x04C8;
        uint64 CapsuleRadius     = 0x04CC;
    }
 
    namespace APawn {
        uint64 PlayerState = 0x0348;
    }
 
    namespace APlayerState {
        uint64 Score             = 0x0318;
        uint64 PlayerId          = 0x031C;
        uint64 Ping              = 0x0320;
        uint64 PlayerNamePrivate = 0x03F8;
        uint64 TeamId            = 0x0598;
    }
 
    namespace ASGPlayerState {
        uint64 ZonePlayerName = 0x0620;
    }
 
    namespace USkeletalMeshComponent {
        uint64 ComponentToWorld                 = 0x0220;
        uint64 MeshRotation                     = 0x01D0;
        uint64 BoneArray                        = 0x0558;
        uint64 CachedComponentSpaceTransforms   = 0x0818;
    }
 
    namespace ASGCharacter {
        uint64 CachedCharacterType       = 0x16AD;
        uint64 AbilitySystemComponent    = 0x17C8;
        uint64 WeaponManagerComponent    = 0x1908;
        uint64 DeathComponent            = 0x1910;
        uint64 Health                    = 0x1BF4;
        uint64 TeamId                    = 0x0598;
        uint64 LastSubmitTime            = 0x032C;
        uint64 LastRenderTime            = 0x0330;
        uint64 CharacterArmorManager     = 0x1AA0;
        uint64 CharacterInventoryManager = 0x1A78;
    }
 
    namespace USGCharacterWeaponManagerComponent {
        uint64 CurrentWeapon = 0x01F8;
        uint64 WeaponList    = 0x0340;
    }
 
    namespace ASGWeapon {
        uint64 WeaponZoomComp     = 0x0B70;
        uint64 WeaponAssembleComp = 0x0B58;
        uint64 WeaponEnemyArray   = 0x0B00;
        uint64 WeaponMeshComp     = 0x0B60;
        uint64 WeaponRecoilComp   = 0x0B88;
        uint64 RecoilData         = 0x0B18;
        uint64 WeaponFiringComp   = 0x0B78;
    }
 
    namespace USGWeaponFiringComponent {
        uint64 ProjectileInitSpeed = 0x0590;
        uint64 FireInitSpeed       = 0x06BC;
    }
 
    namespace USGWeaponRecoilDataAsset_HD {
        uint64 RecoilIntensity            = 0x0308;
        uint64 CameraTranslationIntensity = 0x0314;
        uint64 CameraRotationIntensity    = 0x0320;
    }
 
    namespace USGWeaponRecoilComponent {
        uint64 RepRecoilInfo                    = 0x0108;
        uint64 bEnableRecoil_Pitch              = 0x0158;
        uint64 bEnableRecoil_Yaw                = 0x0159;
        uint64 MaxVerticalRecoil                = 0x0160;
        uint64 MaxHorizontalRecoil              = 0x0164;
        uint64 RecoilRangeSubOffset             = 0x0030;
        uint64 ADSFireRecoilAnimConfig          = 0x04C8;
        uint64 ADSFireRecoilAnimConfigInstance  = 0x0510;
        uint64 FireRecoilAnimConfig             = 0x0528;
        uint64 FireRecoilAnimConfigInstance     = 0x0570;
    }
 
    namespace USGWeaponZoomComponent {
        uint64 ZoomProgressRate   = 0x0404;
        uint64 ScopeMagnification = 0x0578;
        uint64 ScopeADSScale      = 0x0120;
    }
 
    namespace USGWeaponAssembleComponent {
        uint64 CachedMagazine         = 0x0280;
        uint64 AssembledInventoryList = 0x01F8;
    }
 
    namespace USGWeaponContainerComponent {
        uint64 SGWeaponContainer = 0x0900;
        uint64 MaxStack          = 0x0120;
        uint64 ContainList       = 0x0208;
        uint64 StackCountOffset  = 0x0008;
    }
 
    namespace USGCharacterArmorManagerComponent {
        uint64 ArmorList = 0x0278;
    }
 
    namespace USGCharacterInventoryManagerComponent {
        uint64 InventoryArray = 0x0110;
    }
 
    namespace ASGInventory {
        uint64 ItemId              = 0x06C0;
        uint64 CommonDataComponent = 0x0760;
        uint64 AssembleComp        = 0x07F8;
        uint64 ArmorLevel          = 0x06C8;
    }
 
    namespace USGInventoryCommonDataComponent {
        uint64 StandardPrice     = 0x010C;
        uint64 Rarity            = 0x0110;
        uint64 ItemLocalizedName = 0x0170;
        uint64 SimpleDisplayName = 0x0140;
        uint64 ArmorDurability   = 0x0118;
    }
 
    namespace UAbilitySystemComponent {
        uint64 SpawnedAttributes = 0x0188;
    }
 
    namespace HealthAttributes {
        uint64 AttributeSetPtr = 0x08;
        uint64 BodyPartBase    = 0x48;
        int BodyPartCount      = 7;
        int BodyPartStride     = 8;
    }
 
    namespace DeathComponent {
        uint64 bIsDead = 0x0240;
    }
 
    namespace UNetDriver {
        uint64 ClientConnections = 0x0090;
    }
 
    namespace UNetConnection {
        uint64 OpenChannels = 0x0070;
    }
 
    namespace UActorChannel {
        uint64 ChannelActor = 0x0070;
    }
 
    namespace ContainerBase {
        uint64 ContainerMgrComp = 0x08E8;
 
        namespace MgrComponent {
            uint64 ContainerBaseList = 0x0140;
        }
 
        namespace FInventoryContainerBase {
            uint64 ChildActorList = 0x0028;
        }
    }
 
    namespace BoneTransformLayout {
        int Stride = 0x30;
        int RotX = 0x00;
        int RotY = 0x04;
        int RotZ = 0x08;
        int RotW = 0x0C;
        int TransX = 0x10;
        int TransY = 0x14;
        int TransZ = 0x18;
        int ScaleX = 0x20;
        int ScaleY = 0x24;
        int ScaleZ = 0x28;
    }
 
    namespace ASGThrowableProjectile {
        uint64 ProjectileMovementComponent = 0x370;
        uint64 bDestroyOnBounce            = 0x378;
        uint64 bGrenadeBeHold              = 0x4E0;
        uint64 bGrenadeBeThrow             = 0x4E1;
        uint64 TempVelocity                = 0x4E4;
        uint64 PlayEffectTimeForClient     = 0x4F0;
        uint64 FireLocation                = 0x4F4;
        uint64 ServerRealLocation          = 0x618;
        uint64 DefaultGravityScale         = 0x614;
        uint64 ThrowableType               = 0x720;
    }
 
    namespace UProjectileMovementComponent {
        uint64 Velocity               = 0x10C;
        uint64 UpdatedComponent       = 0x0F8;
        uint64 InitialSpeed           = 0x124;
        uint64 MaxSpeed               = 0x128;
        uint64 ProjectileGravityScale = 0x138;
    }
 
    namespace UObject {
        uint64 ObjectID = 0x0020;
    }
}
