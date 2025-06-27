## ✅ Day 16 주요 작업 완료 내용

- QuestData 및 QuestObjective ScriptableObject 정의
- QuestManager 구현 (퀘스트 수락/완료/진행 관리, 보상 처리)
- QuestNPC 구현 (NPC 상호작용으로 퀘스트 수락 및 완료 처리)
- IInteractable 인터페이스 도입 및 ShopNPC 구조 통합 리팩터링
- EnemyController에 questObjectiveId 필드 추가
- DeadState에서 몬스터 사망 시 퀘스트 목표 자동 갱신
- 퀘스트 완료 시 골드 및 아이템 보상 지급, 사운드 및 이펙트 출력
- 샘플 퀘스트 SO (BatQuest) 생성 및 전체 테스트 플로우 검증 완료

## 🔧 주요 스크립트 구성 (Day 15 기준)

- `QuestData`: ScriptableObject, 퀘스트 ID / 이름 / 목표 리스트 / 보상 정보 정의
- `QuestManager`: Singleton, 퀘스트 수락/목표 갱신/완료 처리 및 보상 지급, 상태 관리
- `QuestNPC`: NPC 상호작용 시 퀘스트 수락 또는 완료, 거리 제한 및 로그 출력 포함
- `IInteractable`: NPC 상호작용 통합 인터페이스, QuestNPC/ShopNPC에 적용
- `ShopNPC`: 기존 구조 유지 + IInteractable 구현으로 상호작용 구조 통일
- `EnemyController`: questObjectiveId 필드 추가, 퀘스트 목표 연동 키값 지정
- `DeadState`: 몬스터 사망 시 questObjectiveId 기반으로 목표 자동 갱신
- `GameManager`: 오디오, 이펙트, 로그 출력을 통한 퀘스트 수락/완료 알림 처리
- `PlayerWallet / PlayerInventory`: 퀘스트 보상 연동, 골드 지급 및 아이템 지급 처리에 사용
