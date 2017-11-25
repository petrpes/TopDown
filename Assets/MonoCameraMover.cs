using Components.EventHandler;
using UnityEngine;

public class MonoCameraMover : MonoBehaviour, IEventListener<RoomChangedEventArguments>
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _target;

    private CameraMover _cameraMover;
    private CameraMover CameraMover
    {
        get
        {
            if (_cameraMover == null)
            {
                _cameraMover = new CameraMover(_camera, RoomManager.Instance.CurrentRoom.Rectangle);
                _cameraMover.TargetTransform = _target;
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

