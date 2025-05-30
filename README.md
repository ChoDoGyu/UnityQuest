## ✅ Day 5 주요 작업 완료 내용

- 스킬 시스템 설계 (SkillData [SO], SkillManager)
- 스킬 HUD UI 구현 및 쿨타임 표시 연동
- 원거리 무기 구조(BowWeapon.cs) 및 Projectile 발사 구조 도입
- ObjectPoolManager 범용화 및 투사체 풀 적용
- 무기 장착 시 Skill 자동 연동 + HUD 갱신
- PlayerWeaponManager 통합 (WeaponData 기반 장착 구조)
- 폴더/네임스페이스 구조 정비 (Data, SkillSystem, WeaponSystem, Combat 등)

## 📁 프로젝트 폴더 구조 (Day 5 기준)

Assets/
- ├── 1. Main/
- │   ├── Scripts/
- │   │   ├── Player/
- │   │   │   └── WeaponSystem/
- │   │   │   └── SkillSystem/
- │   │   ├── Data/
- │   │   ├── Core/
- │   │   ├── Combat/
- │   │   ├── InputSystem/
- │   │   ├── SceneManagement/
- │   │   ├── UI/
- │   │   └── Enemy/
- │   ├── Prefabs/
- │   ├── Scenes/
- │   ├── Materials/
- │   ├── Resources/
- │   │   └── Weapons/
- │   └── Settings/
- ├── 2. External/
- ├── Packages/
- └── ProjectSettings/

## 🔧 주요 스크립트 구성 (Day 5 기준)

- `SkillData (SO)`: 스킬 정보 보관 (이름, 쿨타임, 발사체, 속도 등)
- `SkillManager`: 장착된 스킬 관리, 스킬 발동/쿨타임/이벤트 처리
- `Projectile`: 투사체 이동 및 충돌 처리, 풀 반환 구조 포함
- `BowWeapon`: WeaponBase 상속, firePoint에서 Projectile 발사
- `WeaponData (SO)`: 무기 정보와 장착 스킬 연결
- `PlayerWeaponManager`: 무기 프리팹 로드, 장착 및 스킬 연동 제어
- `ObjectPoolManager`: 프리팹 기반 풀링 처리 (투사체 포함)
- `SkillHUD / SkillSlot`: 스킬 UI 표시, 쿨타임 시각화
- `HUDView / UIManager`: 게임 내 HUD 통합 제어


