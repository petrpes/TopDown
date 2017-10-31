using UnityEngine;

[System.Serializable]
public struct HealthPoints
{
    public static HealthPoints Zero = new HealthPoints(0);

    [SerializeField] private ushort _value;

    public ushort Value { get { return _value; } }

    public HealthPoints(ushort value)
    {
        _value = value;
    }

    public static HealthPoints operator +(HealthPoints hp1, HealthPoints hp2)
    {
        return new HealthPoints((ushort)(hp1.Value + hp2.Value));
    }

    public static HealthPoints operator -(HealthPoints hp1, HealthPoints hp2)
    {
        return new HealthPoints((ushort)(hp1.Value - hp2.Value));
    }

    public static bool operator >(HealthPoints hp1, HealthPoints hp2)
    {
        return hp1.Value > hp2.Value;
    }

    public static bool operator <(HealthPoints hp1, HealthPoints hp2)
    {
        return hp1.Value < hp2.Value;
    }

    public static bool operator ==(HealthPoints hp1, HealthPoints hp2)
    {
        return hp1.Value == hp2.Value;
    }

    public static bool operator !=(HealthPoints hp1, HealthPoints hp2)
    {
        return hp1.Value != hp2.Value;
    }
}
