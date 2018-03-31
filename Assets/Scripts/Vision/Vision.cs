using System;
using UnityEngine;

public abstract class Vision : MonoBehaviour
{
    public event Action<GameObject> SpottedImportantObject;

    private GameObject _importantObject;

    private GameObject ImportantObject
    {
        set
        {
            if (!value.SafeEquals(_importantObject))
            {
                _importantObject = value;
                SpottedImportantObject.SafeInvoke(_importantObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsImportant(collision.gameObject))
        {
            ImportantObject = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.Equals(_importantObject))
        {
            ImportantObject = null;
        }
    }

    protected abstract bool IsImportant(GameObject gameObject);
}

