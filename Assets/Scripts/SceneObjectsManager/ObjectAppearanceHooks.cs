using System;

public class ObjectAppearanceHooks : IObjectAppearanceHooks<object>
{
    private Action<ObjectAppearanceType, object> _objectAction;

    public Action<ObjectAppearanceType, object> OnAppearanceAction
    {
        get
        {
            return _objectAction;
        }
        set
        {
            _objectAction = value;
        }
    }
}

