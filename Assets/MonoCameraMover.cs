using Components.EventHandler;
using UnityEngine;

public class MonoCameraMover : MonoBehaviour, IEventListener<RoomChangedEventArguments>
{
    private Transform _target;
    private Camera _camera;

    private CameraMover _cameraMover;
    private CameraMover CameraMover
    {
        get
        {
            if (_cameraMover == null)
            {
                //_target = GameManager.Instance.PlayerInstance.transform;
                _camera = Camera.current;
                _cameraMover = new CameraMover(_camera);
                _cameraMover.TargetTransform = transform;
            }
            return _cameraMover;
        }
    }

    public void HandleEvent(RoomChangedEventArguments arguments, object sender)
    {
        CameraMover.SceneSizeUnits = arguments.CurrentRoom.Rectangle;
    }

    private void Update()
    {
        CameraMover.RecalculatePosition();
    }
}

