using UnityEngine;

public class DestroyCommand : Command
{
    public override void ExecuteCommand(GameObject actor)
    {
        Destroy(gameObject);
    }
}

