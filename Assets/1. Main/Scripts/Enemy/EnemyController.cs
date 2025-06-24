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

        // ���� �ӽ� ����
        private IEnemyState currentState;
        private IEnemyState lastState;      // Stunned �� ���Ϳ�

        // �ʼ� ������Ʈ
        private NavMeshAgent agent;
        private Animator animator;

        // ü�� ����
        private float currentHP;
        private bool isDead = false;

        // ��ġ ����
        private Vector3 spawnPosition;

        // ���� �ν��Ͻ��� (�ܺο��� �����ص� ��)
        public IEnemyState idleState;
        public IEnemyState chaseState;
        public IEnemyState attackState;
        public IEnemyState returnState;
        public IEnemyState deadState;
        public IEnemyState stunnedState;

        // �÷��̾� ������
        public Transform player;

        //EnemyStat ����
        [SerializeField] private EnemyStat statData;

        //ü�¹� UI ������
        [SerializeField] private EnemyHealthBar healthBarPrefab;
        private EnemyHealthBar healthBarInstance;

        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponent<Animator>();

            // ��ġ ����
            spawnPosition = transform.position;

            // ���� �ν��Ͻ� ����
            idleState = new IdleState();
            chaseState = new ChaseState();
            attackState = new AttackState();
            returnState = new ReturnState();
            deadState = new DeadState();
            stunnedState = new StunnedState();
        }

        protected virtual void OnEnable()
        {
            // Ǯ�� ���� �� �ٽ� ���� �� �ʱ�ȭ
            isDead = false;

            if (statData != null)
                currentHP = statData.maxHP;
            else
                currentHP = 100f;

            // ü�¹� Ȱ��ȭ
            if (healthBarInstance != null)
                healthBarInstance.gameObject.SetActive(true);

            // ���� ���� ��ȯ
            TransitionToState(idleState);

            // ������ ����
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

            //ü�¹� UI ����
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
            // �ı��ǰų� Ǯ ��ȯ ���� ������ ����
            EnemyManager.Instance?.UnregisterEnemy(this);
            GameManager.Instance?.MapManager?.UnregisterIcon(this.transform);
        }
    }
}
