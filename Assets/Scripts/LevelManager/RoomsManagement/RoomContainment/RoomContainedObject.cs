using Components.Spawner;
using System;
using UnityEngine;

public class RoomContainedObject : MonoBehaviour, IRoomContainedObject, ISpawnableObject
{
    [SerializeField] private InterfaceComponentCache _defaultRoom;

    public event Action<IRoom, IRoom> RoomChanged;

    //TODO in inspector
    public void SetRoomInInspector(IRoom room)
    {
        _defaultRoom.SetValue(room);
    }

    private IRoom _currentRoom;
    public IRoom CurrentRoom
    {
        get
        {
            return _currentRoom;
        }
        private set
        {
            if (_currentRoom == value)
            {
                return;
            }

            RoomChanged.SafeInvoke(_currentRoom, value);
            _currentRoom = value;
        }
    }

    private void Awake()
    {
        if (_defaultRoom == null ||
            _defaultRoom.GetChachedComponent<IRoom>() == null)
        {
            return;
        }
        CurrentRoom = _defaultRoom.GetChachedComponent<IRoom>();
    }

    private void OnDestroy()
    {
        CurrentRoom = null;
    }

    public void OnAfterSpawn()
    {
        CurrentRoom = LevelManager.Instance.CurrentRoom;
    }

    public void OnBeforeDespawn()
    {
        CurrentRoom = null;
    }
}

