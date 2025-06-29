## ✅ Day 18 주요 작업 완료 내용

- AudioManager 클래스 설계 및 구현 완료
- 씬별 BGM 재생, 스킬 및 UI 효과음 처리, AudioMixer 연동을 통해
- 볼륨 조절과 사운드 관리 시스템을 전체 구조에 통합함.
- SOLID 원칙에 따라 시스템별로 역할 분리하고 확장성 있게 구현 완료.

## 🔧 주요 스크립트 구성 (Day 18 기준)

- `AudioManager`: 전역 싱글톤, BGM/SFX/UI AudioSource 분리, Dictionary 기반 사운드 호출, AudioMixer 연동 및 PlayerPrefs 저장 처리
- `OptionManager`: 슬라이더를 통해 마스터/BGM/SFX/UI 볼륨 제어, AudioManager 호출 및 초기값 불러오기 처리
- `SceneLoader`: 로딩 씬을 거쳐 비동기 씬 전환 처리 + 씬 진입 시 BGM 자동 전환 (StartScene, FieldScene, DungeonScene 등)
- `UIButtonSFX`: UI 버튼 클릭 시 지정된 사운드 이름을 자동 재생, IPointerClickHandler 기반 공용 스크립트
- `SkillData`: AudioClip sfx 필드 추가, 스킬 사용 시 재생할 효과음 클립을 Inspector에서 지정 가능
- `SkillManager`: UseSkill() 시 skill.sfx가 존재하면 AudioManager를 통해 효과음 재생, 스킬 쿨타임 및 발동 처리와 자연스럽게 연동
