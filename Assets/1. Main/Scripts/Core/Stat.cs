using System;
using Main.Scripts.Interfaces;

namespace Main.Scripts.Core
{
    public class Stat : IStat
    {
        public float Current { get; private set; }
        public float Max { get; private set; }

        public Action<float> OnValueChanged;

        public Stat(float max)
        {
            Max = max;
            Current = max;
        }

        public void Increase(float amount) => Set(Current + amount);
        public void Decrease(float amount) => Set(Current - amount);

        public void Set(float value)
        {
            Current = Math.Clamp(value, 0, Max);
            OnValueChanged?.Invoke(Current / Max);
        }

        public void Reset() => Set(Max);
    }
}
