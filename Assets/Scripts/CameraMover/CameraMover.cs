using System;
using System.Collections;
using UnityEngine;

public class CameraMover : MonoBehaviour, ICoroutineCollectionWriter<RoomTransitionArguments>
{
    [SerializeField] private Transform _pursuitedTransform;
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private float _timeOfTransition = 0.5f;

    private Transform _transform;
    private Camera _camera;
    private IRoom _currentRoom;

    private Vector2 _lastPosition;

    private void Awake()
    {
        _transform = transform;
        _camera = GetComponent<Camera>();
        RoomTransitionInvoker.Instance.SubscribeCoroutine(this);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Vector2 cameraSize = CameraSizeGetter.GetCameraSize(_camera);
        float positionZ = _transform.position.z;

        _lastPosition = CameraPositionCalculator.CalculatePosition(cameraSize, SceneRect, 
            _pursuitedTransform.position);

        Vector2 newPosition = Vector2.Lerp(_transform.position, _lastPosition, _speed);
        _transform.position = new Vector3(newPosition.x, newPosition.y, _transform.position.z);
    }

    public IEnumerator Coroutine(Action onComplete, RoomTransitionArguments args)
    {
        _currentRoom = args.NewRoom;
        Vector2 currentPosition = _transform.position;
        while (Mathf.Abs((currentPosition - _lastPosition).magnitude) >= 1f)//TODO const + test if no game break
        {
            yield return new WaitForSeconds(_timeOfTransition);
        }
        onComplete.Invoke();
    }

    private Rect SceneRect
    {
        get
        {
            if (_currentRoom == null)
            {
                return Rect.zero;
            }
            return _currentRoom.Rectangle;
        }
    }
}
