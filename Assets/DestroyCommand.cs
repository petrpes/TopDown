using UnityEngine;

public class DestroyCommand : Command
{
    public override void Execute(GameObject actor)
    {
        Destroy(gameObject);
    }
}

