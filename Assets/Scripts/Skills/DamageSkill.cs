using UnityEngine;

public class DamageSkill : MonoBehaviour
{
    [SerializeField] private HealthPoints _damageValue;
    [SerializeField] private Fraction _fraction;

    public HealthPoints DamageValue
    {
        get { return _damageValue; }
        set { _damageValue = value; }
    }

    /// <summary>
    /// Определяет, к какой фракции относится атакуюший объект.
    /// Объект без прикрепленного класса DamageSkill автоматически
    /// считается принадлежащим к нейтральной фракции, а значит
    /// получает п**ды ото всех остальных фракций
    /// </summary>
    public Fraction CurrentFraction
    {
        get { return _fraction; }
        set { _fraction = value; }
    }
}

