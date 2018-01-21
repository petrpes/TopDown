using UnityEngine;

public static class CommandsExtentions
{
    public static void Execute(this Command[] commands, GameObject actor)
    {
        if (commands == null || commands.Length == 0)
        {
            return;
        }

        for (int i = 0; i < commands.Length; i++)
        {
            commands[i].Execute(actor);
        }
    }
}

