using UnityEngine;

public interface IRoom
{
    Rect Rectangle { get; }
    IDoorsHolder DoorsHolder { get; }
}

