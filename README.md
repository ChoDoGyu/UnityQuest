## ✅ Day 12 주요 작업 완료 내용

- Tooltip UI 설계 및 적용 (아이템/스킬 설명 표시)
- TooltipManager 싱글톤 구조 구현
- TooltipTrigger 컴포넌트 제작 및 슬롯 Prefab에 적용
- ItemSlotBase에 ITooltipProvider 인터페이스 공통 적용
- MapCamera 구성 (Orthographic + RenderTexture 출력)
- RT_MiniMap(RenderTexture) 생성 및 RawImage 출력 연동
- MapManager 설계 (아이콘 등록/제거 및 위치 갱신)
- MiniMap 아이콘 Prefab 제작 및 플레이어/몬스터 자동 등록 연동
- GameManager, EnemyManager 연계로 아이콘 자동 등록 로직 통합

## 🔧 주요 스크립트 구성 (Day 12 기준)

- `TooltipManager`: Singleton, TooltipPanel & TextMeshProUGUI 제어, Show/Hide API 제공
- `TooltipTrigger`: 슬롯(InventorySlot, EquipmentSlot) 등에 추가, 마우스 Hover 시 TooltipManager 호출
- `ItemSlotBase`: ITooltipProvider 인터페이스 구현, Slot마다 아이템 정보 제공
- `MapCameraController`: 플레이어를 따라가는 카메라 컨트롤러, RenderTexture 출력 전용
- `MapManager`: 미니맵 아이콘 등록/제거 관리, 실시간 위치 계산 및 UI 갱신
- `MapIcon`: Target Transform과 RectTransform 연계 구조체, 내부 리스트 관리
- `EnemyManager`: 몬스터 스폰 시 RegisterEnemy로 관리 및 MapManager RegisterIcon 동시 호출
- `GameManager`: 게임 시작 시 플레이어 아이콘 MapManager에 자동 등록, UIManager/PlayerManager 연동 유지
