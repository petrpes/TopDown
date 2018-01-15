using Components.Spawner;
using System;
using UnityEngine;

public class RoomContainedObject : MonoBehaviour, IRoomContainedObject, ISpawnableObject
{
    [SerializeField] private InterfaceComponentCache _defaultRoom;

    public event Action<IRoom> OnObjectAppearInRoom;
    public event Action<IRoom> OnObjectDisappearFromRoom;

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
            if (_currentRoom != null)
            {
                //LevelManager.Instance.RoomContentManager.RemoveObject(_currentRoom, this);
                OnObjectDisappearFromRoom.SafeInvoke(_currentRoom);
            }
            if (value != null)
            {
                //LevelManager.Instance.RoomContentManager.AddObject(value, this);
                OnObjectAppearInRoom.SafeInvoke(value);
            }
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

