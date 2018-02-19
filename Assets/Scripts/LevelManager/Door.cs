using UnityEngine;

public class Door : MonoBehaviour, IDoor
{
    [SerializeField] private Orientation _defaultOrientation;
    [SerializeField] private Collider2D _openedCollider;
    [SerializeField] private Collider2D _closedCollider;
    [HideInInspector]
    [SerializeField] private Orientation _orientation;

    private Transform _transform;

    public float Width
    {
        get
        {
            return RoomConsts.DoorsWidth;
        }
    }

    public IRoom RoomTo { get; set; }

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

    public Orientation Orientation
    {
        get
        {
            return _orientation;
        }
#if UNITY_EDITOR
        set
        {
            _orientation = value;
            var rotationOffset = (int)_defaultOrientation - (int)_orientation;
            transform.Rotate(0, 0, 90f * rotationOffset);
        }
#endif
    }
}

