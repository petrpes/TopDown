using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class RoomEventListener : MonoBehaviour, IRoomEventListener
{
    protected abstract IRoom Room { get; }

    private Dictionary<RoomEventType, string> _actionsNames = new Dictionary<RoomEventType, string>
    {
        { RoomEventType.OnAfterOpen, "OnAfterRoomOpened" },
        { RoomEventType.OnBeforeClose, "OnBeforeRoomClosed" },
        { RoomEventType.OnClear, "OnRoomCleared" },
        { RoomEventType.OnStarted, "OnRoomStarted" }
    };

    private const string OnLevelLoadedActionName = "OnLevelLoaded";
    private const string OnLevelDestroyedActionName = "OnLevelDestroyed";
    private const string OnLevelStartedActionName = "OnAfterLevelStarted";

    private Dictionary<string, MethodInfo> _methodsCache;

    private void Awake()
    {
        if (IsSubscribedOnRoomEvent && Room != null)
        {
            RoomEventHandler.Instance.SubscribeListener(Room, this);
        }
        if (GetMethod(OnLevelLoadedActionName) != null)
        {
            LevelManager.Instance.OnAfterLevelCreated += 
                () => GetMethod(OnLevelLoadedActionName).Invoke(this, null);
        }
        if (GetMethod(OnLevelDestroyedActionName) != null)
        {
            LevelManager.Instance.OnBeforeLevelDestroyed += 
                () => GetMethod(OnLevelDestroyedActionName).Invoke(this, null);
        }
        if (GetMethod(OnLevelStartedActionName) != null)
        {
            LevelManager.Instance.OnAfterLevelStarted +=
                () => GetMethod(OnLevelStartedActionName).Invoke(this, null);
        }
    }

    private void OnDestroy()
    {
        if (IsSubscribedOnRoomEvent && Room != null)
        {
            RoomEventHandler.Instance.UnsubscribeListener(Room, this);
        }
        if (GetMethod(OnLevelLoadedActionName) != null)
        {
            LevelManager.Instance.OnAfterLevelCreated -=
                () => GetMethod(OnLevelLoadedActionName).Invoke(null, null);
        }
        if (GetMethod(OnLevelDestroyedActionName) != null)
        {
            LevelManager.Instance.OnBeforeLevelDestroyed -=
                () => GetMethod(OnLevelDestroyedActionName).Invoke(null, null);
        }
        if (GetMethod(OnLevelStartedActionName) != null)
        {
            LevelManager.Instance.OnAfterLevelStarted -=
                () => GetMethod(OnLevelStartedActionName).Invoke(this, null);
        }
    }

    private bool IsSubscribedOnRoomEvent
    {
        get
        {
            bool result = false;

            foreach (RoomEventType eventType in
                Enum.GetValues(typeof(RoomEventType)))
            {
                result |= GetMethod(eventType) != null;
            }

            return result;
        }
    }

    private MethodInfo GetMethod(RoomEventType eventType)
    {
        return GetMethod(_actionsNames[eventType]);
    }

    private MethodInfo GetMethod(string methodName)
    {
        if (_methodsCache == null)
        {
            _methodsCache = new Dictionary<string, MethodInfo>
                (Enum.GetValues(typeof(RoomEventType)).Length);
        }

        if (!_methodsCache.ContainsKey(methodName))
        {
            var type = GetType();
            BindingFlags flags = BindingFlags.NonPublic |
                BindingFlags.Instance;
            var method = type.GetMethod(methodName, flags);
            _methodsCache.Add(methodName, method);
            return method;
        }

        return _methodsCache[methodName];
    }

    public void OnRoomEvent(RoomEventType eventType)
    {
        if (GetMethod(eventType) != null)
        {
            GetMethod(eventType).Invoke(this, null);
        }//TODO safe call
    }
}

