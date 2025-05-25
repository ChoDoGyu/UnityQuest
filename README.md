## ✅ Day 1 주요 작업 완료 내용

- Unity URP 프로젝트 생성 및 초기 설정 완료
- Input System 패키지 설치 및 PlayerInputActions 구성
- WASD 이동, 마우스 방향 회전 기능 구현
- Player 프리팹화 및 Animator 기본 상태 구성
- PlayerAnimatorController에서 이동 속도 기반 애니메이션 파라미터 처리 구조 설계
- PlayerManager 클래스 도입 (현재는 이동/회전/애니메이션 컴포넌트 연결만 구성된 상태)
- 프로젝트 폴더 구조 정리 (1. Main / 2. External)
- 씬 구성: StartScene, FieldScene

## 📁 프로젝트 폴더 구조 (Day 1 기준)

Assets/
- ├── 1. Main/
- │ ├── Scripts/Player/
- │ ├── Prefabs/
- │ ├── Animations/
- │ ├── Scenes/
- │ ├── Input/
- │ └── Settings/
- ├── 2. External/
- ├── Packages/
- └── ProjectSettings/

## 🔧 Player 관련 스크립트 구성

- `PlayerManager`: 플레이어 관련 모듈 통합 관리 구조 설계 시작
- `PlayerMovement`: Rigidbody 기반 WASD 이동 처리
- `PlayerLook`: 마우스 커서 방향 회전 처리
- `PlayerAnimatorController`: 이동 속도 기반 애니메이션 파라미터 설정
