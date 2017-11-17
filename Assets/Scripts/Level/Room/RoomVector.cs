using System;
using UnityEngine;

namespace Levels.Rooms.RoomCreator
{
    [System.Serializable]
    public struct RoomVector
    {
        [SerializeField] private sbyte _x;
        [SerializeField] private sbyte _y;

        public RoomVector(Vector3 value)
        {
            _x = Conv(value.x);
            _y = Conv(value.y);
        }

        public RoomVector(Vector2 value)
        {
            _x = Conv(value.x);
            _y = Conv(value.y);
        }

        public Vector3 Value3
        {
            set
            {
                _x = Conv(value.x);
                _y = Conv(value.y);
            }
            get
            {
                return new Vector3(
                    Conv(_x),
                    Conv(_y),
                    0);
            }
        }

        public Vector2 Value2
        {
            set
            {
                _x = Conv(value.x);
                _y = Conv(value.y);
            }
            get
            {
                return new Vector2(
                    Conv(_x),
                    Conv(_y));
            }
        }

        private static sbyte Conv(float value)
        {
            return Convert.ToSByte(value);
        }

        private static float Conv(sbyte value)
        {
            return Convert.ToSingle(value);
        }
    }
}

