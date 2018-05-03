using System;

namespace Components.RoomsManager
{
    public class RoomChanger<R> : IRoomsChanger<R> where R : class
    {
        private R _followingRoom;

        public event Action<R> OnBeforeRoomClosed;
        public event Action<R> OnAfterRoomOpened;

        private IRoomTransition<R> _transitionHandler;

        public R CurrentRoom { get; private set; }

        public RoomChanger(IRoomTransition<R> transitionHandler)
        {
            _transitionHandler = transitionHandler;
        }

        public bool ChangeRoom(R room)
        {
            if (_transitionHandler != null && _transitionHandler.IsInProcess)
            {
                return false;
            }

            if (CurrentRoom != null)
            {
                if (OnBeforeRoomClosed != null)
                {
                    OnBeforeRoomClosed.Invoke(CurrentRoom);
                }
            }

            _followingRoom = room;

            if (_transitionHandler == null)
            {
                if (OnAfterRoomOpened != null)
                {
                    OnAfterRoomOpened.Invoke(CurrentRoom);
                }
            }
            else
            {
                _transitionHandler.InvokeTransitionAction(CurrentRoom, room, RoomAfterTransitionAction);
            }

            return true;
        }

        public void Dispose()
        {
            CurrentRoom = default(R);
            _followingRoom = default(R);
        }

        private void RoomAfterTransitionAction()
        {
            CurrentRoom = _followingRoom;

            if (OnAfterRoomOpened != null)
            {
                OnAfterRoomOpened.Invoke(CurrentRoom);
            }
        }
    }
}

