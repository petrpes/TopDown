using UnityEngine;

public class TestCommand : Command
{
    public string _text = "1";

    public override void ExecuteCommand(GameObject actor)
    {
        Debug.Log(_text);
    }
}

