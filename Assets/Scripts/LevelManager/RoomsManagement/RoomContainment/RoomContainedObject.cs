using Components.Spawner;
using System;
using UnityEngine;

[RequireComponent(typeof(RoomEventListenerProxy))]
public class RoomContainedObject : MonoBehaviour, IRoomContainedObject, ISpawnableObject
{
    [SerializeField] private InterfaceComponentCache _defaultRoom;

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
        CurrentRoom = null;
    }

    private IRoom _currentRoom;
    public IRoom CurrentRoom
    {
        get
        {
            if (_currentRoom == null && _defaultRoom != null && 
                _defaultRoom.GetChachedComponent<IRoom>() != null)
            {
                _currentRoom = _defaultRoom.GetChachedComponent<IRoom>();
                LevelManager.Instance.RoomContentManager.AddObject(_currentRoom, this);
            }

            return _currentRoom;
        }

        private set
        {
            if (value.SafeEquals(CurrentRoom))
            {
                return;
            }

            RoomChanged.SafeInvoke(_currentRoom, value);

            if (value == null)
            {
                LevelManager.Instance.RoomContentManager.RemoveObject(_currentRoom, this);
            }
            else
            {
                LevelManager.Instance.RoomContentManager.AddObject(value, this);
            }

            _currentRoom = value;
        }
    }
}

