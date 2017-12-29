using System;

public interface IRoomTransition
{
    void TransitionToRoom(IRoom oldRoom, IRoom newRoom, Action onComplete);
}

