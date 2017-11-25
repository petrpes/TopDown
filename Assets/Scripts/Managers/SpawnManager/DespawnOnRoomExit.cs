using Components.EventHandler;
using UnityEngine;

public class DespawnOnRoomExit : MonoBehaviour, IEventListener<RoomChangedEventArguments>
{
    [SerializeField] private MonoBehaviour _spawnedComponent;

    private void OnEnable()
    {
        TypeEventManager.Instance.SubscribeListener(this);
    }

    private void OnDisable()
    {
        TypeEventManager.Instance.UnSubscribeListener(this);
    }

    public void HandleEvent(RoomChangedEventArguments arguments, object sender)
    {
        if (arguments.PreviousRoom != null)
        {
            if (_spawnedComponent == null)
            {
                SpawnManager.Instance.Despawn(_spawnedComponent);
            }
            else
            {
                SpawnManager.Instance.Despawn(gameObject);
            }
        }
    }
}

