using UnityEngine;

public class VisionMoveController : MoveController
{
    [SerializeField] private Vision _vision;
    private Transform _transform;
    private Transform _pursued;

    private void Awake()
    {
        if (_vision == null)
        {
            return;
        }
        _vision.SpottedImportantObject += OnObjectSpotted;
    }

    private void OnObjectSpotted(GameObject gameObject)
    {
        if (gameObject == null)
        {
            _pursued = null;
            return;
        }
        _pursued = gameObject.transform;
    }

    public override bool GetControl(out DirectionVector direction)
    {
        if (_pursued == null)
        {
            direction = DirectionVector.Zero;
            return false;
        }
        if (_transform == null)
        {
            _transform = transform;
        }
        direction = new DirectionVector(_pursued.position - _transform.position);
        return true;
    }
}
