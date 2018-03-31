using System;
using UnityEngine;

public abstract class StateMachine<M> : MonoBehaviour where M : class
{
    private IState<M> _state;

    public event Action<IState<M>> OnStateChanged;

    public IState<M> CurrentState
    {
        get { return _state; }
        protected set
        {
            if (_state != null)
            {
                _state.BeforeReset(this as M);
            }

            if (value != null)
            {
                _state = value;
                if (OnStateChanged != null)
                {
                    OnStateChanged.Invoke(_state);
                }
                _state.AfterSet(this as M);
            }
        }
    }
}

