using UnityEngine;

public abstract class MoveController : MonoBehaviour
{
    public abstract bool GetControl(out DirectionVector direction);
}
