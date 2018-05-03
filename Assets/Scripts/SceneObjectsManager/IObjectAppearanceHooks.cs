using System;

/// <summary>
/// Sending the event of any object's creation on a scene
/// </summary>
/// <typeparam name="O">Object type</typeparam>
public interface IObjectAppearanceHooks<O>
{
    Action<ObjectAppearanceType, O> OnAppearanceAction { get; set; }
}

/// <summary>
/// Invokes action when current object's creation invoked
/// </summary>
public interface IObjectAppearanceListener
{
    void OnAppearanceAction(ObjectAppearanceType type);
}

public enum ObjectAppearanceType
{
    Created,
    Appeared,
    Disappeared,
    Destroyed
}

