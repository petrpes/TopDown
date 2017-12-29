using System;
using UnityEngine;

public class OnOpenSpawnObject : MonoBehaviour, IRoomEventListener
{
    [SerializeField] private SpawnableObjectsProxy _object;

    public Action this[RoomEventType eventType]
    {
        get
        {
            if (eventType.Equals(RoomEventType.OnAfterOpen))
            {
                return ExecuteRoomAction;
            }
            return null;
        }
    }

    public void ExecuteRoomAction()
    {
        GameObject newObject = SpawnManager.Instance.Spawn(_object).gameObject;

        newObject.transform.position = transform.position;
        newObject.transform.position = transform.position;
        newObject.transform.parent = transform.parent;

        Destroy(gameObject);
    }
}

