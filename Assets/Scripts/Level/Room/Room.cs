using System;
using UnityEngine;

namespace Levels.Rooms.RoomCreator
{
    public class Room : ScriptableObject
    {
        [SerializeField] private IdPositionPair[] _objects;
        [SerializeField] private RoomRect _roomSize;

        public IdPositionPair[] Objects
        {
            get
            {
                return _objects;
            }
            set
            {
                _objects = value;
            }
        }

        public RoomRect RoomSize
        {
            get
            {
                return _roomSize;
            }
            set
            {
                _roomSize = value;
            }
        }

        [Serializable]
        public class IdPositionPair
        {
            public PrefabId Id;
            public RoomVector Position;
        }
    }
}

