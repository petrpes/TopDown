using System;
using UnityEngine;

[Serializable]
public struct Fraction
{
    public static Fraction Friendly = new Fraction() { _fraction = FractionEnum.Friend };
    public static Fraction Enemy = new Fraction() { _fraction = FractionEnum.Enemy };
    public static Fraction Neutral = new Fraction() { _fraction = FractionEnum.Neutral };

    [SerializeField] FractionEnum _fraction;

    public static Fraction operator !(Fraction fraction)
    {
        return new Fraction()
        {
            _fraction = ReverseFraction(fraction._fraction)
        };
    }

    public bool IsHittableBy(Fraction fraction)
    {
        switch (_fraction)
        {
            case FractionEnum.Friend: return fraction._fraction != _fraction;
            case FractionEnum.Enemy: return fraction._fraction != _fraction;
            case FractionEnum.Neutral: return true;
        }

        return false;
    }

    private static FractionEnum ReverseFraction(FractionEnum fractionEnum)
    {
        switch (fractionEnum)
        {
            case FractionEnum.Friend: return FractionEnum.Enemy;
            case FractionEnum.Enemy: return FractionEnum.Friend;
        }
        return fractionEnum;
    }

    public override bool Equals(object obj)
    {
        return (obj is Fraction) && (((Fraction)obj)._fraction == _fraction);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    private enum FractionEnum : byte
    {
        Friend,
        Enemy,
        Neutral
    }
}

