using Main.Scripts.Core;

namespace Main.Scripts.Player
{
    public class PlayerStat
    {
        public Stat HP { get; private set; }
        public Stat Stamina { get; private set; }

        public PlayerStat(float maxHp, float maxStamina)
        {
            HP = new Stat(maxHp);
            Stamina = new Stat(maxStamina);
        }
    }
}
