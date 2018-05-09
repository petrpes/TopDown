public class ObjectAppearanceListenerInvoker : IObjectAppearanceListener
{
    private object _baseObject;

    public ObjectAppearanceListenerInvoker(object baseObject)
    {
        _baseObject = baseObject;
    }

    public void OnAppearanceAction(ObjectAppearanceType type)
    {
        ObjectsAPI.Hooks.OnAppearanceAction.SafeInvoke(type, _baseObject);
    }
}

