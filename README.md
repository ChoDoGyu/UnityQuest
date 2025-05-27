## ✅ Day 3 주요 작업 완료 내용

- Stat 시스템 및 HUD 연동
- HUD UI 구성 및 연결
- Debug Console 도입 (F1 키 토글)
- Manager 구조 리팩토링
- GameManager 중심 통합 구조 완성

## 📁 프로젝트 폴더 구조 (Day 3 기준)

Assets/
- ├── 1. Main/
- │   ├── Scripts/
- │   │   ├── Player/
- │   │   ├── Core/
- │   │   ├── InputSystem/
- │   │   ├── SceneManagement/
- │   └── CameraSystem/
- │   ├── Prefabs/
- │   ├── Animations/
- │   ├── Scenes/
- │   ├── Input/
- │   ├── Materials/
- │   └── Settings/
- ├── 2. External/
- ├── Packages/
- └── ProjectSettings/

## 🔧 주요 스크립트 구성 (Day 3 기준)

- `GameManager`: 모든 시스템 초기화 및 통합 제어, 중앙 허브 역할 수행
- `InputManager`: Unity Input System 연동 및 Singleton 패턴 구성
- `UIManager`: HUDView, DebugConsoleView를 종합 관리하는 UI 컨트롤러
- `HUDView`: HP / Stamina 슬라이더 값 갱신 처리
- `DebugConsoleView`: F1 키로 활성화, 텍스트 로그 출력
- `PlayerManager`: 플레이어 모듈 통합 제어 (이동, 회전, 애니메이션, 스탯)
- `PlayerStat`: 체력/스태미나 관리용 일반 클래스
- `Stat`: 공통 스탯 클래스 (이벤트 기반 수치 변화 통보)
- `IStat`: 스탯 인터페이스, SOLID 원칙 일부 적용
- `PlayerMovement`: Rigidbody 기반 WASD 이동 처리
- `PlayerLook`: 마우스 기준 회전 처리 (RaycastAll, LateUpdate(), groundMask 사용)
- `PlayerAnimatorController`: 이동 속도 기반 애니메이션 파라미터 설정
- `CameraRaycaster`: 카메라 → 플레이어 사이 장애물 감지 후 TransparentObstacle 머티리얼 적용
- `SceneLoader`: 씬 간 전환 처리, 비동기 로딩 구조 포함
