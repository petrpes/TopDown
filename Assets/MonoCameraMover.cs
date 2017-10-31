using UnityEngine;

public class MonoCameraMover : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Rect _sceneSizeInUnits;
    [SerializeField] private Transform _target;

    private CameraMover _cameraMover;

    private void Start()
    {
        _cameraMover = new CameraMover(_camera, _sceneSizeInUnits);
        _cameraMover.TargetTransform = _target;
    }

    private void Update()
    {
        _cameraMover.RecalculatePosition();
    }
}

