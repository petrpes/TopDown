using System;
using UnityEngine;

public class CallbackCollector
{
    private int _pendingCallback;
    private Action _onCallbacksFinished;
    private bool _isStopped;

    public Action AddCallback(Action onCallback = null)
    {
        _isStopped = false;
        _pendingCallback++;

        return () =>
        {
            if (!_isStopped)
            {
                onCallback.SafeInvoke();
                OnCallback();
            }
        };
    }

    public void SetReady(Action onFinish)
    {
        if (onFinish == null)
        {
            throw new ArgumentNullException("onFinish is null");
        }

        _onCallbacksFinished = onFinish;
        CheckCallback();
    }

    private void OnCallback()
    {
        _pendingCallback--;
        CheckCallback();
    }

    private void CheckCallback()
    {
        if (_pendingCallback < 0)
        {
            Debug.LogError("");
        }
        if (_pendingCallback == 0)
        {
            _onCallbacksFinished.SafeInvoke();
            _onCallbacksFinished = null;
        }
    }

    public bool IsRunning
    {
        get
        {
            return _pendingCallback > 0;
        }
    }

    public void ForceStop()
    {
        _isStopped = true;
        _pendingCallback = 0;
    }
}

