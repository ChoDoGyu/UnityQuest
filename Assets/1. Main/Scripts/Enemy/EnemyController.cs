using UnityEngine;
using UnityEngine.AI;
using Main.Scripts.Interfaces;
using Main.Scripts.Data;

namespace Main.Scripts.Enemy
{
    public class EnemyController : MonoBehaviour, IDamageable
    {
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

        private void Awake()
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

        private void Start()
        {
            if (statData != null)
            {
                agent.speed = statData.moveSpeed;
                currentHP = statData.maxHP;
            }
            else
            {
                Debug.LogWarning($"[EnemyController] {name} �� statData ���Ҵ��.");
                currentHP = 100f;
            }

            if (player == null)
                player = GameObject.FindGameObjectWithTag("Player")?.transform;

            // ���� ���´� Idle
            TransitionToState(idleState);

            // EnemyManager�� ���
            EnemyManager.Instance?.RegisterEnemy(this);
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

        public void TakeDamage(float damage)
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
            EnemyManager.Instance?.UnregisterEnemy(this);
        }
    }
}
