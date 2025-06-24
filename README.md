## ✅ Day 13 주요 작업 완료 내용

- 필드/마을 환경 배경 오브젝트 및 바닥 타일 에셋 적용
- Portal Prefab 제작 및 Portal.cs 구현
- SceneLoader 확장: 로딩씬 연동 구조 설계 및 적용
- DungeonScene 기본 구성 및 NavMesh Bake
- Player가 Portal Trigger 진입 시 LoadingScene → DungeonScene 전환 흐름 구축

## 🔧 주요 스크립트 구성 (Day 13 기준)

- `Portal`: Trigger Collider 감지 후 SceneLoader 호출 (LoadScene), LoadingScene 경유
- `SceneLoader`: Singleton, DontDestroyOnLoad, LoadScene 메서드로 LoadingScene + 대상 씬 Async Load 처리
