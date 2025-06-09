## ✅ Day 7 주요 작업 완료 내용

- 4종 몬스터 모델 및 애니메이션 적용 (Skeleton, Bat, Ghost, Rabbit)
- Animator Controller 구성: Idle / Run / Attack / Hit / Die 상태 트리 + 파라미터 트리거 기반 제어
- 몬스터 프리팹 통합 구성: NavMeshAgent, Animator, CapsuleCollider, EnemyController 구성 완료
- EnemyController 설계 및 상태머신 구현 (SOLID 기반 IEnemyState 설계 반영)
- 몬스터 AI 상태: Idle, Chase, Attack, Stunned, Return, Dead 총 6가지 상태 구현
- EnemyManager 도입: 몬스터 등록/해제 및 전체 추적/관리 가능 구조
- EnemyStat (ScriptableObject) 설계: 몬스터별 능력치 데이터화 (HP, 이동속도, 사거리, 스턴시간 등)
- 모든 State 클래스에서 하드코딩된 값 제거 → EnemyStat 연동 구조 통일
- 사망 처리 방식 개선 (코루틴 기반 지연 제거 → 풀링 대응 고려 가능)
- 플레이어 자동 추적 기능 도입 (GameObject.FindWithTag("Player"))
- 애니메이션 스케일 조정 (FBX Import 설정에서 Scale Factor 조정 방식 적용)

## 📁 프로젝트 폴더 구조 (Day 7 기준)

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

## 🔧 주요 스크립트 구성 (Day 7 기준)

- `EnemyController`: 상태머신 기반 몬스터 제어, 데미지 처리, 스폰 위치 기억, 플레이어 추적 등
- `EnemyStat(SO)`: 몬스터 능력치 관리 (HP, 속도, 쿨타임, 범위 등 데이터화)
- `EnemyManager`: 몬스터 전역 등록/해제 및 전체 리스트 추적 (싱글톤)
- `IEnemyState`: SOLID 기반 상태 인터페이스
- `IdleState ~ DeadState`: 각 몬스터 상태에 대응하는 독립 클래스
