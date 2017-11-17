using UnityEngine;

public class BuffAccelerationCommand : Command
{
    [SerializeField] private float _buff;

    public override void Execute(GameObject actor)
    {
        BuffHandler buffHandler = actor.GetComponent<BuffHandler>();
        if (buffHandler != null)
        {
            buffHandler.BuffParameter(BuffType.AccelerationTime, _buff, gameObject);
        }
    }
}

