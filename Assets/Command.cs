using Components.Common;
using UnityEngine;

public abstract class Command : MonoBehaviour, IMutable, IResetable
{
    public bool IsMuted { get; set; }

    public void Execute(GameObject actor)
    {
        if (!IsMuted)
        {
            ExecuteCommand(actor);
        }
    }

    public abstract void ExecuteCommand(GameObject actor);

    public void Reset()
    {
        IsMuted = false;
    }
}

