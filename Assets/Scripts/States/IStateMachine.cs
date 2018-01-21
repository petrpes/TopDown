using System;

public interface IStateMachine
{
    ObjectState CurrentState { get; }
    void UpdateMachine();
}

public struct ObjectState
{
    public byte Id { get; private set; }

    public ObjectState(byte id)
    {
        Id = id;
    }
}

