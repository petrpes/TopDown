using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    [SerializeField] private Vector2 _defaultOrientation;
    [SerializeField] private Collider2D _openedCollider;
    [SerializeField] private Collider2D _closedCollider;
    [SerializeField] private InterfaceComponentCache _targetRoom;

    private Transform _transform;

    public float Width
    {
        get
        {
            return RoomConsts.DoorsWidth;
        }
    }

    public IRoom RoomTo
    {
        get { return _targetRoom.GetChachedComponent<IRoom>(); }
        set { _targetRoom.SetValue(value); }
    }

    public bool IsOpened
    {
        get
        {
            if (_openedCollider.enabled &&
                _closedCollider.enabled)
            {
                _closedCollider.enabled = false;
            }
            return _openedCollider.enabled;
        }
        set
        {
            if (value)
            {
                _openedCollider.enabled = true;
                _closedCollider.enabled = false;
            }
            else
            {
                _openedCollider.enabled = false;
                _closedCollider.enabled = true;
            }
        }
    }
    public Vector2 DefaultOrientation
    {
        get { return _defaultOrientation; }
    }
}

