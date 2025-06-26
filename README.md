## ✅ Day 15 주요 작업 완료 내용

- PlayerWallet 시스템 구현
- PlayerInventory 구조 설계
- InventoryManager UI 분리
- ShopController 상점 기능 구축
- ShopItemSlot 툴팁 및 클릭 처리
- ShopNPC 상호작용 구조 설계
- 상점 UI + 인벤토리 UI 동시 연동
- 판매 시스템 구축
- 툴팁 확장

## 🔧 주요 스크립트 구성 (Day 15 기준)

- `PlayerWallet`: 골드 소지 처리, Add/Spend/Check, OnGoldChanged 이벤트 발행
- `PlayerInventory`: 아이템 보유 데이터 구조, Add/Remove/Clear, OnInventoryChanged 이벤트 발행
- `InventoryManager`: 슬롯 및 소지금 UI 구성, Player에서 전달받은 정보를 바탕으로 View만 구성
- `ShopController`: 상점 열기/닫기, 아이템 구매 처리, ShopData 바인딩
- `ShopItemSlot`: 슬롯 클릭 시 ShopController에 구매 요청, TooltipTrigger 구현
- `UIManager`: 상점 UI 및 인벤토리 UI 상태 제어, isSellMode 상태 관리 포함
- `InventorySlot`: isSellMode일 때 클릭 시 판매 처리, 골드 증가 및 아이템 제거
- `ItemSlotBase`: 공통 툴팁 텍스트 반환 메서드 구현, 판매가 조건부 표시 처리
- `ItemData`: 가격 필드 추가 (price), 판매가 계산 기준 제공
