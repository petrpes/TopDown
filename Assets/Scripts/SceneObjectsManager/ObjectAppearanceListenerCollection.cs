using System.Collections.Generic;

public class ObjectAppearanceListenerCollection : IObjectAppearanceListener
{
    public IEnumerable<IObjectAppearanceListener> _collection;

    public ObjectAppearanceListenerCollection(IEnumerable<IObjectAppearanceListener> collection)
    {
        _collection = collection;
    }

    public void OnAppearanceAction(ObjectAppearanceType type)
    {
        foreach (var listener in _collection)
        {
            listener.OnAppearanceAction(type);
        }
    }
}

