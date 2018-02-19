using UnityEngine;

public class LevelEventListenerCommand : MonoBehaviour
{
    [SerializeField] private Command[] _onLevelCreatedCommands;
    [SerializeField] private Command[] _onLevelStartedCommands;
    [SerializeField] private Command[] _onLevelDestroyedCommands;

    private void Awake()
    {
        if (_onLevelCreatedCommands != null && _onLevelCreatedCommands.Length > 0)
        {
            LevelManager.Instance.OnAfterLevelCreated += OnLevelCreated;
        }
        if (_onLevelStartedCommands != null && _onLevelStartedCommands.Length > 0)
        {
            LevelManager.Instance.OnAfterLevelStarted += OnLevelStarted;
        }
        if (_onLevelDestroyedCommands != null && _onLevelDestroyedCommands.Length > 0)
        {
            LevelManager.Instance.OnBeforeLevelDestroyed += OnLevelDestroyed;
        }
    }

    private void OnDestroy()
    {
        if (_onLevelCreatedCommands != null && _onLevelCreatedCommands.Length > 0)
        {
            LevelManager.Instance.OnAfterLevelCreated -= OnLevelCreated;
        }
        if (_onLevelStartedCommands != null && _onLevelStartedCommands.Length > 0)
        {
            LevelManager.Instance.OnAfterLevelStarted -= OnLevelStarted;
        }
        if (_onLevelDestroyedCommands != null && _onLevelDestroyedCommands.Length > 0)
        {
            LevelManager.Instance.OnBeforeLevelDestroyed -= OnLevelDestroyed;
        }
    }

    private void OnLevelCreated()
    {
        if (isActiveAndEnabled)
        {
            _onLevelCreatedCommands.Execute(gameObject);
        }
    }

    private void OnLevelStarted()
    {
        if (isActiveAndEnabled)
        {
            _onLevelStartedCommands.Execute(gameObject);
        }
    }

    private void OnLevelDestroyed()
    {
        if (isActiveAndEnabled)
        {
            _onLevelDestroyedCommands.Execute(gameObject);
        }
    }
}

