using UnityEngine;

namespace Levels.Rooms.RoomCreator
{
    [System.Serializable]
    public struct RoomRect
    {
        [SerializeField] private RoomVector _position;
        [SerializeField] private RoomVector _size;

        public RoomRect(Rect rect)
        {
            _position = new RoomVector(rect.position);
            _size = new RoomVector(rect.size);
        }

        public Rect Value
        {
            set
            {
                _position = new RoomVector(value.position);
                _size = new RoomVector(value.size);
            }
            get
            {
                return new Rect(_position.Value2, _size.Value2);
            }
        }
    }
}

