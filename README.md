## ✅ Day 10 주요 작업 완료 내용

- ESC 메뉴 UI 구성
- Option 설정 UI 구성
- 기능 연동 완료
- PauseManager 개선

## 🔧 주요 스크립트 구성 (Day 9 기준)

- `AudioManager`: AudioMixer 제어, SFX Dictionary 관리, 볼륨 Get/Set API 제공
- `OptionManager`: 사운드 슬라이더 → AudioManager 연동, 해상도 드롭다운 초기화/변경 처리, 키 리맵 탭 전환 제어
- `PauseManager`: ESC 입력 Toggle, PauseMenu UI On/Off, OptionMenu 열기 함수 포함
- `RebindSlot`: PerformInteractiveRebinding + 중복 검사, InputField로 키 입력 대기
- `PlayerInputActions`: Move(Composite), Attack, Potion, Skill1~4 액션 추가 및 Binding
- `UIManager`: OptionMenu, PauseMenu 등 UI 활성화/비활성화 제어
