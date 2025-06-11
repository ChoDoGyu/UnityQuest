## ✅ Day 9 주요 작업 완료 내용

- 보스 몬스터 전용 구조 적용
- 몬스터 원거리 공격형으로 전환
- 모든 몬스터 체력바 UI 연동
- Animator 상태 전이 오류 해결

## 🔧 주요 스크립트 구성 (Day 9 기준)

- `BossController`: EnemyController 확장, 보스용 FSM 연동
- `BossPatternManager`: 체력 퍼센트 기반 Stage 전환 구조
- `RangedEnemyController`: 투사체 발사 위치/프리팹 보유
- `RangedAttackState`: 거리 유지 + 투사체 발사 AI 상태
- `EnemyHealthBar`: 체력바 UI 갱신 및 회전 처리
- `EnemyController`: 체력바 연동, virtual 구조 개선 포함
