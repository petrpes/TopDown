using Components.Spawner;
using System;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(RoomEventListenerProxy))]
public class RoomContainedObject : MonoBehaviour, IRoomContainedObject, ISpawnableObject
{
    [SerializeField] private InterfaceComponentCache _defaultRoom;

    public event Action<IRoom, IRoom> RoomChanged;

    public IRoom DefaultRoom
    {
        private get
        {
            return _defaultRoom.GetChachedComponent<IRoom>();
        }
#if UNITY_EDITOR
        set
        {
            if (_defaultRoom == null)
            {
                _defaultRoom = new InterfaceComponentCache();
            }
            _defaultRoom.SetValue(value);
        }
#endif
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
                DefaultRoom != null)
            {
                _currentRoom = DefaultRoom;
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

[CustomEditor(typeof(RoomContainedObject))]
public class RoomContainedObjectEditor : Editor
{
    SerializedProperty _lastProp;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (_lastProp == null || !_lastProp.Equals(this.Property("_defaultRoom")))
        {
            _lastProp = this.Property("_defaultRoom");
        }
    }
}



