using Main.Scripts.Enemy;

namespace Main.Scripts.Interfaces
{
    public interface IEnemyState
    {
        void EnterState(EnemyController enemy);
        void UpdateState(EnemyController enemy);
        void ExitState(EnemyController enemy);
    }
}
