namespace Main.Scripts.Interfaces
{
    public interface IStat
    {
        float Current { get; }
        float Max { get; }

        void Increase(float amount);
        void Decrease(float amount);
        void Set(float value);
        void Reset();
    }
}
