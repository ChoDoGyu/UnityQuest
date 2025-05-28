## ✅ Day 4 주요 작업 완료 내용

- 무기 시스템 설계 (WeaponType, IWeapon, WeaponBase)
- SwordWeapon.cs 기반 근접 공격 기능 구현 (Trigger 충돌 + 데미지 전달)
- PlayerWeaponManager 도입 및 PlayerManager 연동
- IDamageable 인터페이스 정의 및 충돌 데미지 분리 구조화
- 무기 프리팹(Resources 기반) 자동 장착 구조 구현
- 폴더 구조 및 네임스페이스 정비 (Interfaces, WeaponSystem, Resources/Weapons)

## 📁 프로젝트 폴더 구조 (Day 4 기준)

Assets/
- ├── 1. Main/
- │   ├── Scripts/
- │   │   ├── Player/
- │   │   │   └── WeaponSystem/
- │   │   ├── Core/
- │   │   ├── InputSystem/
- │   │   ├── SceneManagement/
- │   │   ├── UI/
- │   │   └── Enemy/
- │   └── CameraSystem/
- │   ├── Prefabs/
- │   ├── Scenes/
- │   ├── Materials/
- │   └── Settings/
- ├── 2. External/
- ├── Packages/
- └── ProjectSettings/

## 🔧 주요 스크립트 구성 (Day 4 기준)

- `PlayerManager`: 무기 입력 통합 (1~3번 키 → EquipWeapon)
- `PlayerWeaponManager`: 무기 로드/파괴/장착 로직 전담
- `WeaponType`: 무기 종류 열거형 (Sword, Bow, Staff)
- `IWeapon`: 모든 무기의 공통 인터페이스
- `WeaponBase`: 장착/해제 구조 구현 (추상 메서드 Attack 포함)
- `SwordWeapon`: WeaponBase 상속, Collider 기반 근접 공격 구현
- `IDamageable`: 데미지 전달 인터페이스 (적용 대상 확장 가능)


