using System;
using System.Collections;
using UnityEngine;

public class TestRoom : MonoBehaviour, IRoom, ICoroutineCollectionWriter<RoomTransitionArguments>
{
    [SerializeField] private Vector2 _size;
    private Transform _transform;
    private Rect _rectangle;

    public Rect Rectangle
    {
        get
        {
            if (_transform == null)
            {
                _transform = transform;
            }
            if (_rectangle == Rect.zero)
            {
                _rectangle = new Rect(_transform.position.x - _size.x / 2,
                                      _transform.position.y - _size.y / 2,
                                      _size.x,
                                      _size.y);
            }

            return _rectangle;
        }
    }

    public IEnumerator Coroutine(Action onComplete, RoomTransitionArguments args)
    {
        if (args.OldRoom != null && Equals(args.OldRoom))
        {
            gameObject.SetActive(false);
        }
        if (args.NewRoom != null && Equals(args.NewRoom))
        {
            gameObject.SetActive(true);
        }
        onComplete.Invoke();

        yield return null;
    }

    private void Awake()
    {
        RoomTransitionInvoker.Instance.SubscribeCoroutine(this);
    }
}

