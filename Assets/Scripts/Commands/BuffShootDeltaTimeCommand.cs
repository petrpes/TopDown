using UnityEngine;

public class BuffShootDeltaTimeCommand : Command
{
    [SerializeField] private float _buff;

    public override void ExecuteCommand(GameObject actor)
    {
        BuffHandler buffHandler = actor.GetComponent<BuffHandler>();
        if (buffHandler != null)
        {
            buffHandler.BuffParameter(BuffType.ShootDeltaTime, _buff, gameObject);
        }
    }
}

