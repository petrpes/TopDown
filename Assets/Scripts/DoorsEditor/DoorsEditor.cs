using UnityEngine;

public class DoorsEditor : IDoorsEditor
{
    private GameObject _wallsBefore = null;
    private BoxCollider2D[] _collidersCache;
    private Rect _roomRect;

    public GameObject AddDoor(GameObject walls, int wallId, float length, IRoom roomTo, Transform parent)
    {
        if (_wallsBefore == null || !walls.Equals(_wallsBefore))
        {
            _wallsBefore = walls;

        }
        return null;
    }

    private void CacheColliders(GameObject walls)
    {
        _collidersCache = walls.GetComponents<BoxCollider2D>();
        _roomRect = new Rect();
    }

    private void SortColliders()
    {

    }
}

