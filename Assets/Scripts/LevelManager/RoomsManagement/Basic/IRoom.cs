using UnityEngine;

public interface IRoom
{
    IShape Shape { get; }
    IDoorsHolder DoorsHolder { get; }
}

