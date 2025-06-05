## ✅ Day 6 주요 작업 완료 내용

- StatType 기반 스탯 구조 통합 (HP, Attack, Stamina, Exp, Level 등)
- ExpTable 도입 (경험치 요구량 수식 기반)
- 경험치 획득 → 레벨업 → 스탯 증가까지 통합 처리
- PlayerStat.ApplyLevelUpBonus() 분리 적용
- GameManager 중심의 PlayLevelUpEffects(Vector3 pos) 구조 정립
- FXManager / AudioManager 신규 도입 및 GameManager 통합
- 레벨업 이펙트 (Hun0FX) 및 사운드 (CasualGameSFX) 에셋 연동
- 무기 프리팹 구조 통일 (검/활/스태프 Common, Uncommon 6종)
- WeaponData SO 기반 무기 구조 확립 및 PlayerWeaponManager 통합 적용
- 플레이어 캐릭터 교체 (UnityChan 적용 및 애니메이션 베이스 세팅 완료)

## 📁 프로젝트 폴더 구조 (Day 6 기준)

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

## 🔧 주요 스크립트 구성 (Day 6 기준)

- `PlayerStat`: 스탯 통합 관리, 레벨업 시 보너스 적용
- `ExpTable`: 경험치 요구량 수식 기반 계산
- `PlayerManager`: 경험치 획득/레벨업/스탯 상승 연계
- `WeaponBase / MeleeWeapon / RangedWeapon`: 근접/원거리 무기 통합 구조
- `WeaponData (SO)`: 무기 정보와 프리팹/스킬 연동 데이터화
- `PlayerWeaponManager`: 무기 장착 및 Skill 자동 연결
- `FXManager`: 레벨업 이펙트 재생, Pool 없이 Instantiate 방식
- `AudioManager`: 효과음 재생 전용 매니저, 이름 기반 등록 구조
- `GameManager`: FX/SFX 재생 포함 통합 중앙 매니저
- `HUDView / UIManager`: HUD 갱신 대응 (HP, Stamina 등)


