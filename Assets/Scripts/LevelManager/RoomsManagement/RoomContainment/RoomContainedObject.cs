using Components.Spawner;
using System;
using UnityEngine;

public class RoomContainedObject : MonoBehaviour, IRoomContainedObject, ISpawnableObject
{
    [SerializeField] private InterfaceComponentCache _defaultRoom;

    [NonSerialized] private bool _isRoomSet = false;

    public event Action<IRoom, IRoom> RoomChanged;

    //TODO in inspector
    public void SetRoomInInspector(IRoom room)
    {
        _defaultRoom.SetValue(room);
    }

    public void OnAfterSpawn()
    {
        CurrentRoom = LevelManager.Instance.CurrentRoom;
    }

    public void OnBeforeDespawn()
    {
    }

    private IRoom _currentRoom;
    public IRoom CurrentRoom
    {
        get
        {
            if (!_isRoomSet && _defaultRoom != null && 
                _defaultRoom.GetChachedComponent<IRoom>() != null)
            {
                _currentRoom = _defaultRoom.GetChachedComponent<IRoom>();
            }

            _isRoomSet = true;
            return _currentRoom;
        }

        private set
        {
            if (value.Equals(CurrentRoom))
            {
                return;
            }

            RoomChanged.SafeInvoke(_currentRoom, value);
            _currentRoom = value;
        }
    }
}

