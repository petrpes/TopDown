using Components.EventHandler;
using UnityEngine;

/// <summary>
/// Переключение между комнатами уровня
/// </summary>
public class RoomManager : IEventListener<LevelChangedEventArguments>
{
    public static RoomManager Instance = new RoomManager();

    private int _currentRoomId = -1;
    private RoomChangedEventArguments _eventArguments;

    private RoomManager()
    {
        TypeEventManager.Instance.SubscribeListener(this);
        _eventArguments = new RoomChangedEventArguments();
        ResetRoomId();
    }

    private Level CurrentLevel { get { return LevelManager.Instance.CurrentLevel; } }

    public IRoom CurrentRoom
    {
        get
        {
            return _currentRoomId >= 0 ?
                CurrentLevel.RoomLibrary[_currentRoomId] : 
                null;
        }
    }

    public int CurrentRoomId
    {
        set
        {
            _eventArguments.PreviousRoom = CurrentRoom;
            _currentRoomId = value;
            _eventArguments.CurrentRoom = CurrentRoom;

            TypeEventManager.Instance.Notify(_eventArguments, this);
        }
    }

    public void HandleEvent(LevelChangedEventArguments arguments, object sender)
    {
        ResetRoomId();
    }

    private void ResetRoomId()
    {
        CurrentRoomId = CurrentLevel == null ? -1 : CurrentLevel.StartingRoomId;
    }
}

public class RoomChangedEventArguments : IEventArguments
{
    public IRoom PreviousRoom;
    public IRoom CurrentRoom;
}

