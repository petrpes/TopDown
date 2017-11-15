using System;

public static class ActionExtentions
{
    public static void SafeInvoke(this Action action)
    {
        if (action != null)
        {
            action.Invoke();
        }
    }

    public static void SafeInvoke<T>(this Action<T> action, T arg)
    {
        if (action != null)
        {
            action.Invoke(arg);
        }
    }
}

