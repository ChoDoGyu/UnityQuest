## ✅ Day 2 주요 작업 완료 내용

- PlayerManager 구조 확정 및 자동 연결 방식으로 책임 정리 (GetComponent<> + RequireComponent 적용)
- Cinemachine 패키지 설치 및 쿼터뷰 카메라 구조 적용
- CM_QuarterViewCam 가상 카메라를 Player/CameraRig/CM_QuarterViewCam 구조로 계층화
- Main Camera에 CinemachineBrain + Tag 설정
- 장애물 가림 처리 구현 (Raycast → Obstacle Layer에만 반응 → 투명 머티리얼 교체 → 복원 처리)
- CameraRaycaster.cs 설계 및 Layer 기반 필터링 방식 확정

## 📁 프로젝트 폴더 구조 (Day 2 기준)

Assets/
- ├── 1. Main/
- │   ├── Scripts/
- │   │   ├── Player/
- │   │   ├── Core/               ★ GameManager.cs
- │   │   ├── InputSystem/        ★ InputManager.cs
- │   │   ├── SceneManagement/    ★ SceneLoader.cs
- │   └── CameraSystem/       ★ CameraRaycaster.cs
- │   ├── Prefabs/
- │   ├── Animations/
- │   ├── Scenes/
- │   ├── Input/
- │   ├── Materials/              ★ TransparentObstacle.mat
- │   └── Settings/
- ├── 2. External/
- ├── Packages/
- └── ProjectSettings/

## 🔧 주요 스크립트 구성 (Day 2 기준)

- `PlayerManager`: PlayerMovement, PlayerLook, PlayerAnimatorController 자동 연결 및 Instance 제공 (싱글톤)
- `PlayerMovement`: Rigidbody 기반 WASD 이동 처리
- `PlayerLook`: 마우스 기준 회전 처리 (RaycastAll, LateUpdate(), groundMask 사용)
- `PlayerAnimatorController`: 이동 속도 기반 애니메이션 파라미터 설정
- `CameraRaycaster`: 카메라 → 플레이어 사이 장애물 감지 후 TransparentObstacle 머티리얼 적용
- `GameManager`: 전체 게임 흐름 통제 (기본 구조만 확정, 후속 확장 예정)
- `InputManager`: Unity Input System 연동 및 Singleton 패턴 구성
- `SceneLoader`: 씬 간 전환 처리, 비동기 로딩 구조 포함

## 📷 카메라 구성

- Cinemachine Virtual Camera 사용 (CM_QuarterViewCam)
- Follow / Look At 대상: Player
- Follow Offset: (0, 10, -10) → 쿼터뷰 고정 시점 구성
- Binding Mode: World Space
- Player → CameraRig → CM_QuarterViewCam 계층 구조 구성
