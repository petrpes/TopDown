using UnityEngine;

public class CameraMover
{
    //TODO camera size getter
    private Vector3 _targetPosition;
    private Transform _targetTransform;
    private Camera _camera;
    private Rect _sceneSizeUnits;
    private Transform _cameraTransform;
    private Vector2 _cameraSize;
    private float _cameraZPosition;

    public Transform TargetTransform { set { _targetTransform = value; } }
    public Rect SceneSizeUnits
    {
        set
        {
            bool shouldRecalculate = _sceneSizeUnits != value;
            _sceneSizeUnits = value;
            if (shouldRecalculate)
            {
                RecalculatePosition();
            }
        }
    }

    public CameraMover(Camera camera, Rect sceneSizeUnits)
    {
        _camera = camera;
        _sceneSizeUnits = sceneSizeUnits;
        _cameraTransform = _camera.transform;
        _cameraZPosition = _cameraTransform.position.z;
    }

    public void RecalculatePosition()
    {
        bool isCameraSizeChanged = CheckCameraSizeIsChanged();
        bool isTargetPositionChanged = CheckIfTargetPositionChanged();

        if (isCameraSizeChanged || isTargetPositionChanged)
        {
            Vector2 cameraHalfSize = _cameraSize / 2f;
            Vector2 sceneHalfSize = _sceneSizeUnits.size / 2f;

            float positionX = Mathf.Min(cameraHalfSize.x, sceneHalfSize.x);
            float positionY = Mathf.Min(cameraHalfSize.y, sceneHalfSize.y);
            float sizeX = Mathf.Max(_sceneSizeUnits.size.x - _cameraSize.x, 0);
            float sizeY = Mathf.Max(_sceneSizeUnits.size.y - _cameraSize.y, 0);

            Rect cameraMovingRectX = new Rect(positionX, 0, sizeX, _sceneSizeUnits.size.y);
            Rect cameraMovingRectY = new Rect(0, positionY, _sceneSizeUnits.size.x, sizeY);

            cameraMovingRectX.position += _sceneSizeUnits.position;
            cameraMovingRectY.position += _sceneSizeUnits.position;

            Vector3 cameraPosition = Vector3.forward * _cameraZPosition;

            if (cameraMovingRectX.Contains(_targetPosition))
            {
                cameraPosition.x = _targetPosition.x;
            }
            else
            {
                cameraPosition.x = 
                    _targetPosition.x < cameraMovingRectX.min.x ? 
                    cameraMovingRectX.min.x :
                    cameraMovingRectX.max.x;
            }

            if (cameraMovingRectY.Contains(_targetPosition))
            {
                cameraPosition.y = _targetPosition.y;
            }
            else
            {
                cameraPosition.y = 
                    _targetPosition.y < cameraMovingRectY.min.y ?
                    cameraMovingRectY.min.y :
                    cameraMovingRectY.max.y;
            }

            _cameraTransform.position = cameraPosition;
        }
    }

    private bool CheckCameraSizeIsChanged()
    {
        float height =2f * _camera.orthographicSize;
        float width = height * _camera.aspect;

        Vector2 newCameraSize = new Vector2(width, height);
        if (newCameraSize != _cameraSize)
        {
            _cameraSize = newCameraSize;
            return true;
        }
        return false;
    }

    private bool CheckIfTargetPositionChanged()
    {
        if (_targetPosition != _targetTransform.position)
        {
            _targetPosition = _targetTransform.position;
            return true;
        }
        return false;
    }
}

