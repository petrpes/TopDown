using UnityEngine;

public class BuffSpeedCommand : Command
{
    [SerializeField] private float _buff;

    public override void Execute(GameObject actor)
    {
        BuffHandler buffHandler = actor.GetComponent<BuffHandler>();
        if (buffHandler != null)
        {
            buffHandler.BuffParameter(BuffType.Speed, _buff, gameObject);
        }
    }
}

