using System;
using System.Collections.Generic;

public interface IDoorsHolder
{
    IEnumerable<IDoor> GetDoors(Func<IDoor, DoorPosition, bool> predicate = null);
    DoorPosition GetDoorPosition(IDoor door);
}

[System.Serializable]
public struct DoorPosition
{
    public int LineId;
    public float PartOfTheLine;

    public override string ToString()
    {
        return "Line Id: " + LineId + "; Positon : " + PartOfTheLine;
    }
}

