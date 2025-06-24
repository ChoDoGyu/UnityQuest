using UnityEngine;
using UnityEngine.AI;
using Main.Scripts.Interfaces;
using Main.Scripts.Data;
using Main.Scripts.Core;

namespace Main.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour, IDamageable
    {
        public System.Action OnReturnedToPool;

        // 상태 머신 관련
        private IEnemyState currentState;
        private IEnemyState lastState;      // Stunned 후 복귀용

        // 필수 컴포넌트
        private NavMeshAgent agent;
        private Animator animator;

        // 체력 관련
        private float currentHP;
        private bool isDead = false;

        // 위치 관련
        private Vector3 spawnPosition;

        // 상태 인스턴스들 (외부에서 주입해도 됨)
        public IEnemyState idleState;
        public IEnemyState chaseState;
        public IEnemyState attackState;
        public IEnemyState returnState;
        public IEnemyState deadState;
        public IEnemyState stunnedState;

        // 플레이어 추적용
        public Transform player;

        //EnemyStat 연동
        [SerializeField] private EnemyStat statData;

        //체력바 UI 프리팹
        [SerializeField] private EnemyHealthBar healthBarPrefab;
        private EnemyHealthBar healthBarInstance;

        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            // 위치 저장
            spawnPosition = transform.position;

            // 상태 인스턴스 생성
            idleState = new IdleState();
            chaseState = new ChaseState();
            attackState = new AttackState();
            returnState = new ReturnState();
            deadState = new DeadState();
            stunnedState = new StunnedState();
        }

        protected virtual void OnEnable()
        {
            // 풀링 복귀 후 다시 켜질 때 초기화
            isDead = false;

            if (statData != null)
                currentHP = statData.maxHP;
            else
                currentHP = 100f;

            // 체력바 활성화
            if (healthBarInstance != null)
                healthBarInstance.gameObject.SetActive(true);

            // 최초 상태 전환
            TransitionToState(idleState);

            // 아이콘 재등록
            EnemyManager.Instance?.RegisterEnemy(this);
        }

        protected virtual void Start()
        {
            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player")?.transform;

            if (healthBarPrefab != null && healthBarInstance == null)
            {
                Transform cam = Camera.main.transform;
                Vector3 offset = new Vector3(0, 2.0f, 0);
                healthBarInstance = Instantiate(healthBarPrefab, transform.position + offset, Quaternion.identity);
                healthBarInstance.Initialize(cam);
                healthBarInstance.transform.SetParent(transform, worldPositionStays: true);
            }
        }

        private void Update()
        {
            currentState?.UpdateState(this);
        }

        public void TransitionToState(IEnemyState newState)
        {
            if (isDead) return;

            currentState?.ExitState(this);
            currentState = newState;
            currentState?.EnterState(this);
        }

        public virtual void TakeDamage(float damage)
        {
            if (isDead) return;

            currentHP -= damage;

            if (currentHP <= 0)
            {
                isDead = true;
                TransitionToState(deadState);
            }
            else
            {
                lastState = currentState;
                TransitionToState(stunnedState);
            }

            //체력바 UI 갱신
            if (healthBarInstance != null)
            {
                healthBarInstance.UpdateHP(currentHP, statData.maxHP);
            }
        }


        public void RecoverFromStun()
        {
            TransitionToState(lastState);
        }

        public Vector3 GetSpawnPosition() => spawnPosition;
        public NavMeshAgent GetAgent() => agent;
        public Animator GetAnimator() => animator;
        public EnemyStat GetStat() => statData;

        private void OnDestroy()
        {
            // 파괴되거나 풀 반환 전에 아이콘 제거
            EnemyManager.Instance?.UnregisterEnemy(this);
            GameManager.Instance?.MapManager?.UnregisterIcon(this.transform);
        }
    }
}
