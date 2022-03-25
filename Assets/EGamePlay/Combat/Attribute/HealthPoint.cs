using System.Collections.Generic;
using UnityEngine;

namespace EGamePlay.Combat
{
    public sealed class HealthPoint : Entity
    {
        public FloatNumeric HealthPointNumeric;
        public FloatNumeric HealthPointMaxNumeric;
        public float Value { get => (float)HealthPointNumeric.Value; }
        public float MaxValue { get => (float)HealthPointMaxNumeric.Value; }

        public override void Awake()
        {
            base.Awake();
            HealthPointNumeric = AddChild<FloatNumeric>();
            HealthPointMaxNumeric = AddChild<FloatNumeric>();
        }

        public void CheckValue()
        {
            if (IsFull())
            {
                Reset();
            }
        }
        public void SetBaseValue(float value)
        {
            HealthPointNumeric.SetBase(value);
            CheckValue();
        }
        public void Reset()
        {
            HealthPointNumeric.SetBase(HealthPointMaxNumeric.Value);
        }

        public void SetBaseMaxValue(float value)
        {
            
            HealthPointMaxNumeric.SetBase(value);
        }

        public void Minus(float value)
        {
            HealthPointNumeric.MinusBase(value);
            CheckValue();
        }

        public void Add(float value)
        {
            HealthPointNumeric.AddBase(value);
            CheckValue();
        }

        public float Percent()
        {
            return (float)Value / MaxValue;
        }

        public float PercentHealth(float pct)
        {
            return (float)(MaxValue * pct);
        }

        public bool IsFull()
        {
            return Value >= MaxValue;
        }

    }
}