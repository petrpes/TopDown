using System.Collections.Generic;

public interface ILevel
{
    IRoom StartRoom { get; }
    ILevelMap LevelMap { get; }
    IEnumerable<object> DefaultObjects { get; }
}

