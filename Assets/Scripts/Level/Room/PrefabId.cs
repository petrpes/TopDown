using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Levels.Rooms.RoomCreator
{
    [System.Serializable]
    public struct PrefabId
    {
        [SerializeField] private ushort _prefabId;

        public PrefabId(int prefabId)
        {
            if (Check(prefabId))
            {
                _prefabId = Convert.ToUInt16(prefabId);
            }
            else
            {
                _prefabId = 0;
            }
        }

        public int Value
        {
            get
            {
                return _prefabId;
            }
            set
            {
                if (Check(value))
                {
                    _prefabId = Convert.ToUInt16(value);
                }
                else
                {
                    _prefabId = 0;
                }
            }
        }

        private static bool Check(int id)
        {
            if (id <= ushort.MinValue || id >= ushort.MaxValue)
            {
                Debug.LogError("Id should be in range with ushort type");
                return false;
            }
            return true;
        }
    }
}

