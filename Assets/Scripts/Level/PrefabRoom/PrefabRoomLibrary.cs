using UnityEngine;

public class PrefabRoomLibrary : MonoBehaviour, IRoomLibrary
{
    [SerializeField] private PrefabRoom[] _room;

    public IRoom this[int roomId] { get { return _room[roomId]; } }

    public int Count { get { return _room.Length; } }
}

