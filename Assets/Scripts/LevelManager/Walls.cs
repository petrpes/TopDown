using System.Collections.Generic;
using UnityEngine;

public class Walls : WallsBase
{
    ICollidersCutter _collidersCutter;
    Dictionary<Orientation, ICollection<Collider2D>> _collidersCollection;

    public override void AddWall(Orientation orientation, Collider2D collider)
    {
        if (_collidersCollection == null)
        {
            _collidersCollection = new 
                Dictionary<Orientation, ICollection<Collider2D>>
                (StructsExtentions.EnumLength<Orientation>());
        }

        _collidersCollection.Add(orientation, 
            new List<Collider2D>(byte.MaxValue) { collider });
    }

    public override void Dispose()
    {
        _collidersCollection = null;
    }

    public override bool AddDoor(Orientation orientation, float position, float length)
    {
        return _collidersCutter.CutColliders(orientation, position, 
            length, _collidersCollection[orientation]);
    }

    public override bool RemoveDoor(Orientation orientation, float position)
    {
        return _collidersCutter.UniteColliders(orientation, position,
            _collidersCollection[orientation]);
    }
}

public interface ICollidersCutter
{
    bool CutColliders(Orientation orientation, float position, float length, 
        ICollection<Collider2D> colliders);
    bool UniteColliders(Orientation orientation, float position,
        ICollection<Collider2D> colliders);
}

