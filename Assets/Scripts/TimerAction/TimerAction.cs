using Components.Timer;
using UnityEngine;

public class TimerAction : MonoBehaviour
{
    [SerializeField] private float _time;
    [SerializeField] private bool _isIntervalTimer;
    [SerializeField] private Command[] _commands;

    public float Time { set { _time = value; } }

    private IntervalTimer _intervalTimer;
    private ExpirationTimer _expirationTimer;
    private ITimer _currentTimer;

    public void StartTimer()
    {
        
    }

    private void OnEnable()
    {
        if (_currentTimer == null)
        {
            if (_isIntervalTimer)
            {
                _intervalTimer = new IntervalTimer(_time);
                _intervalTimer.OnTickTimer += OnTimerTick;
                _currentTimer = _intervalTimer;
            }
            else
            {
                _expirationTimer = new ExpirationTimer(_time);
                _expirationTimer.OnExpiredTimer += OnTimerTick;
                _currentTimer = _expirationTimer;
            }
        }
        _currentTimer.Start();
    }

    private void OnDisable()
    {
        _currentTimer.Stop();
    }

    private void OnTimerTick()
    {
        _commands.Execute(gameObject);
    }
}

