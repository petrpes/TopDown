using System;
using System.Collections;
using UnityEngine;

public class CameraMover : MonoBehaviour, IRoomTransition
{
    [SerializeField] private Mover _pursuitedMover;
    [SerializeField] private Transform _pursuitedTransform;
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private float _timeOfTransition = 0.5f;

    private Transform _transform;
    private Camera _camera;
    private IRoom _currentRoom;

    private Vector2 _desiredPosition;
    private bool _isInProcess = false;
    private bool _isPaused = false;
    private Coroutine _coroutine;

    private void Awake()
    {
        _transform = transform;
        _camera = GetComponent<Camera>();
        LevelAPIs.TransitionHandler.SubscribeTransistor(this, null);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        if (_isPaused)
        {
            return;
        }

        CalculateDesiredPosition();

        Vector3 oldPosition = _transform.position;
        Vector2 newPosition = Vector2.Lerp(oldPosition, _desiredPosition, _speed);
        _transform.position = new Vector3(newPosition.x, newPosition.y, oldPosition.z);
    }

    private void CalculateDesiredPosition()
    {
        Vector2 cameraSize = CameraSizeGetter.GetCameraSize(_camera);

        _desiredPosition = CameraPositionCalculator.CalculatePosition(cameraSize, RoomRect, PursuedPosition);
    }

    private Vector3 PursuedPosition
    {
        get
        {
            return _pursuitedMover == null ? 
                (_pursuitedTransform == null ? (Vector3)RoomRect.center : _pursuitedTransform.position) :
                _pursuitedMover.Position;
        }
    }

    public void InvokeTransitionAction(IRoom oldRoom, IRoom newRoom, Action onComplete)
    {
        _coroutine = StartCoroutine(CoroutineTransition(oldRoom, newRoom, onComplete));
    }

    public IEnumerator CoroutineTransition(IRoom oldRoom, IRoom newRoom, Action onComplete)
    {
        _isInProcess = true;

        _currentRoom = newRoom;
        CalculateDesiredPosition();

        while (Mathf.Abs(((Vector2)_transform.position - _desiredPosition).magnitude) >= 1f)//TODO const + test if no game break
        {
            yield return new WaitForSeconds(_timeOfTransition);
        }

        _isInProcess = false;
        onComplete.Invoke();
    }

    public void ForceStop()
    {
        if (_coroutine != null && _isInProcess)
        {
            StopCoroutine(_coroutine);
        }
        _isInProcess = false;
    }

    public void Pause()
    {
        _isPaused = true;
    }

    public void UnPause()
    {
        _isPaused = false;
    }

    private Rect RoomRect
    {
        get
        {
            if (_currentRoom == null)
            {
                return Rect.zero;
            }
            var room = _currentRoom.Shape.Rectangle;
            var rect = new Rect(room.position - RoomConsts.WallsSize - RoomConsts.VisibleEdgeSize,
                room.size + (RoomConsts.WallsSize + RoomConsts.VisibleEdgeSize) * 2f);
            return rect;
        }
    }

    public bool IsInProcess { get { return _isInProcess; } }
}
