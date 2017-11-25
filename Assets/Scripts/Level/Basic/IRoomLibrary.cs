public interface IRoomLibrary
{
    IRoom this[int roomId] { get; }
    int Count { get; }
}

