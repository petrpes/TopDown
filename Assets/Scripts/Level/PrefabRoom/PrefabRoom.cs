using UnityEngine;

public abstract class PrefabRoom : MonoBehaviour, IRoom
{
    [SerializeField] private Vector2 _size;

    private Rect _rectangle;
    public Rect Rectangle
    {
        get
        {
            if (_rectangle == Rect.zero)
            {
                if (_size == Vector2.zero)
                {
                    Debug.Log("Size of the room should not be zero");
                }
                else
                {
                    _rectangle = new Rect(transform.position, _size);
                }
            }

            return _rectangle;
        }
    }
}

