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

    public static void SafeInvoke<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
    {
        if (action != null)
        {
            action.Invoke(arg1, arg2);
        }
    }
}

