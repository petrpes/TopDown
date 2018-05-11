public interface IPublicRoomEventListener
{
    IRoomEventListener Listener { get; }
    bool ShouldListenAllRooms { get; }
}

