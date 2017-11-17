using UnityEngine;

public class DestroyCommand : Command
{
    [SerializeField] private GameObject _mainBody;

    public override void Execute(GameObject actor)
    {
        if (_mainBody == null)
        {
            _mainBody = gameObject;
        }
        Destroy(_mainBody);
    }
}

