## ✅ Day 8 주요 작업 완료 내용

- 드롭 아이템 시스템 설계 및 구현
- 몬스터 드롭 처리 로직 구현
- 플레이어 상호작용 구현
- ScriptableObject 및 프리팹 구성

## 📁 프로젝트 폴더 구조 (Day 8 기준)

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

## 🔧 주요 스크립트 구성 (Day 8 기준)

- `ItemData`: 아이템 공통 데이터 정의용 ScriptableObject
- `DropTable`: 몬스터 드롭 확률 및 목록 정의용 ScriptableObject
- `EnemyDropHandler`: 몬스터 사망 시 드롭 처리 전담 컴포넌트
- `ItemPickup`: 플레이어와 충돌 시 드롭 아이템 효과 적용 처리
- `DeadState`: 사망 후 드롭 처리 시점 통합 (지연 코루틴 내부에서 DropItems 호출)
