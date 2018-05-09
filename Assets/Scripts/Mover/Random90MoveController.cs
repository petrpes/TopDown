using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random90MoveController : MoveController
{
    [SerializeField] private float _minTime;
    [SerializeField] private float _maxTime;

    private Coroutine _coroutine;
    private bool _isRunning;
    private DirectionVector2 _currentDirection;
    [System.NonSerialized] private float _lastTime;

    public override bool GetControl(out DirectionVector direction)
    {
        if (!_isRunning)
        {
            _isRunning = true;
            _coroutine = StartCoroutine(ChangeDirection());
        }

        direction = _currentDirection.ToDirectionVector();
        return _isRunning;
    }

    private IEnumerator ChangeDirection()
    {
        while (_isRunning)
        {
            float randAngle = 90f * Random.Range(0, 4);
            _currentDirection = new DirectionVector2(randAngle.ToVector2(1f));
            _lastTime = Random.Range(_minTime, _maxTime);
            yield return new WaitForSeconds(_lastTime);
        }
    }
}

