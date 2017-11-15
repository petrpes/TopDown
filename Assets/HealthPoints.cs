using UnityEngine;

[System.Serializable]
public struct HealthPoints
{
    public static HealthPoints Zero = new HealthPoints(0);

    [SerializeField] private int _value;

    public int Value { get { return _value; } }

    public HealthPoints(int value)
    {
        _value = value;
    }

    public static HealthPoints operator +(HealthPoints hp1, HealthPoints hp2)
    {
        return new HealthPoints(hp1.Value + hp2.Value);
    }

    public static HealthPoints operator -(HealthPoints hp1, HealthPoints hp2)
    {
        return new HealthPoints(hp1.Value - hp2.Value);
    }

    public static bool operator >(HealthPoints hp1, HealthPoints hp2)
    {
        return hp1.Value > hp2.Value;
    }

    public static bool operator >=(HealthPoints hp1, HealthPoints hp2)
    {
        return hp1.Value >= hp2.Value;
    }

    public static bool operator <(HealthPoints hp1, HealthPoints hp2)
    {
        return hp1.Value < hp2.Value;
    }

    public static bool operator <=(HealthPoints hp1, HealthPoints hp2)
    {
        return hp1.Value <= hp2.Value;
    }

    public static bool operator ==(HealthPoints hp1, HealthPoints hp2)
    {
        return hp1.Value == hp2.Value;
    }

    public static bool operator !=(HealthPoints hp1, HealthPoints hp2)
    {
        return hp1.Value != hp2.Value;
    }

    public static HealthPoints Max(HealthPoints hp1, HealthPoints hp2)
    {
        return hp1.Value > hp2.Value ? hp1 : hp2;
    }

    public static HealthPoints Min(HealthPoints hp1, HealthPoints hp2)
    {
        return hp1.Value < hp2.Value ? hp1 : hp2;
    }
}
