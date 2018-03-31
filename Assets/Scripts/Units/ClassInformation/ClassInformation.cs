using UnityEngine;

public class ClassInformation : MonoBehaviour
{
    [SerializeField] private Fraction _fraction;

    /// <summary>
    /// Определяет, к какой фракции относится атакуюший объект.
    /// Объект без данного класса автоматически
    /// считается принадлежащим к нейтральной фракции, а значит
    /// получает п**ды ото всех остальных фракций
    /// </summary>
    public Fraction CurrentFraction
    {
        get { return _fraction; }
        set { _fraction = value; }
    }
}
