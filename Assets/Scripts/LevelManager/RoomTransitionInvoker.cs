using System;

public class RoomTransitionInvoker : IRoomTransition
{
    public static RoomTransitionInvoker Instance = new RoomTransitionInvoker();

    private CoroutineCollection<RoomTransitionArguments> _coroutineCollection;
    private RoomTransitionArguments _arguments;

    private RoomTransitionInvoker()
    {
        _coroutineCollection = new CoroutineCollection<RoomTransitionArguments>();
    }

    public void SubscribeCoroutine(ICoroutineCollectionWriter<RoomTransitionArguments> collectionWriter)
    {
        _coroutineCollection.AddCoroutine(collectionWriter);
    }

    public void TransitionToRoom(IRoom oldRoom, IRoom newRoom, Action onComplete)
    {
        if (_coroutineCollection.IsRunning)
        {
            _coroutineCollection.ForceStop();
        }

        if (_arguments == null)
        {
            _arguments = new RoomTransitionArguments();
        }
        _arguments.OldRoom = oldRoom;
        _arguments.NewRoom = newRoom;

        _coroutineCollection.Run(onComplete, _arguments);
    }
}

public class RoomTransitionArguments
{
    public IRoom OldRoom { get; set; }
    public IRoom NewRoom { get; set; }
}

