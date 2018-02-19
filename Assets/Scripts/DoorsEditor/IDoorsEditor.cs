using UnityEngine;

public interface IDoorsEditor
{
    /// <summary>
    /// Creating door on the wall
    /// </summary>
    /// <param name="walls">Walls game object</param>
    /// <param name="wallId">0 - top, 1 - right, etc.</param>
    /// <param name="length">length from the beginning of the wall (clockwise)</param>
    /// <param name="roomTo">Room to go to</param>
    /// <param name="parent">Parent transform</param>
    GameObject AddDoor(GameObject walls, int wallId, float length, IRoom roomTo, Transform parent);
}

