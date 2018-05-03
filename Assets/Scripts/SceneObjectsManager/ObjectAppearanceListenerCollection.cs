using System.Collections.Generic;

public class ObjectAppearanceListenerCollection : IObjectAppearanceListener
{
    public IEnumerable<IObjectAppearanceListener> _collection;
    private bool _shouldSendEvents;
    private object _baseObject;

    public ObjectAppearanceListenerCollection(IEnumerable<IObjectAppearanceListener> collection, object baseObject = null)
    {
        _collection = collection;
        _shouldSendEvents = baseObject != null;
        _baseObject = baseObject;
    }

    public void OnAppearanceAction(ObjectAppearanceType type)
    {
        if (_shouldSendEvents)
        {
            ObjectsAPI.Hooks.OnAppearanceAction.SafeInvoke(type, _baseObject);
        }

        foreach (var listener in _collection)
        {
            listener.OnAppearanceAction(type);
        }
    }
}

