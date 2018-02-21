using UnityEngine;

public interface IWallsColliderCreator
{
    bool CreateColliders(IShape shape, IDoorsHolder holder, GameObject gameObject);
}

