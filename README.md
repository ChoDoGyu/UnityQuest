## ✅ Day 11 주요 작업 완료 내용

- 인벤토리 UI 구성
- 장비창 UI 구성
- Drag & Drop 시스템 구축
- EquipmentSlotManager 설계
- EquipmentManager 개선
- PlayerInputActions 연동

## 🔧 주요 스크립트 구성 (Day 11 기준)

- `InventoryManager`: Bag Panel에 InventorySlot Prefab 자동 Instantiate, Slot List 관리, Add/Remove/Sort 기능 제공
- `InventorySlot`: 드래그 시작/종료 처리, DragHandler 연동, 우클릭 시 EquipmentSlotManager 통해 슬롯 자동 등록
- `EquipmentSlot`: SlotType별 아이템 등록, EquipmentManager 장착 호출, Clear 시 자동 해제
- `EquipmentSlotManager`: Armor/Weapon/Accessory 슬롯 리스트 관리, 타입별 슬롯 찾기 제공
- `EquipmentManager`: 장착 아이템 외형 Attach + 플레이어 Stat 갱신, 해제 시 외형 Destroy & Stat 원복
- `DragHandler`: DragIcon UI 관리, 드래그 시작/종료 시 아이콘 상태 & 위치 제어
- `UIManager`: PlayerInputActions 직접 보유, I 키로 InventoryPanel & EquipmentPanel 동시 토글, 기존 PauseMenu/OptionMenu 연동 유지
