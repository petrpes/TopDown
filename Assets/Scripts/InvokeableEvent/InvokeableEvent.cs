using System;

public class InvokeableEvent<T>
{
    public event Action<T> Event;

    public void InvokeEvent(T param)
    {
        if (Event != null)
        {
            Event.Invoke(param);
        }
    }

    public bool IsEmpty
    {
        get
        {
            return Event == null;
        }
    }
}

public class InvokeableEvent
{
    public event Action Event;

    public void InvokeEvent()
    {
        if (Event != null)
        {
            Event.Invoke();
        }
    }

    public bool IsEmpty
    {
        get
        {
            return Event == null;
        }
    }
}

public class InvokeableEvent<T1, T2>
{
    public event Action<T1, T2> Event;

    public void InvokeEvent(T1 param1, T2 param2)
    {
        if (Event != null)
        {
            Event.Invoke(param1, param2);
        }
    }

    public bool IsEmpty
    {
        get
        {
            return Event == null;
        }
    }
}

