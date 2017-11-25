using Components.EventHandler;
using System;

/// <summary>
/// Переключение между уровнями
/// </summary>
public class LevelManager
{
    public static LevelManager Instance = new LevelManager();

    private ILevelCreator _levelCreator;
    private ILevelDestroyer _levelDestroyer;
    private Level _currentLevel;
    private LevelChangedEventArguments _arguments;

    private LevelManager()
    {
        _levelCreator = PrefabRoomLevelCreator.Instance;
        _levelDestroyer = PrefabRoomLevelCreator.Instance;
        _arguments = new LevelChangedEventArguments();
        _currentLevel = null;
    }

    public Level CurrentLevel { get { return _currentLevel; } }

    public void DestroyCurrentLevel(Action onLevelDestroyed)
    {
        if (_currentLevel != null)
        {
            _levelDestroyer.DestroyLevel(_currentLevel, () =>
            {
                _currentLevel = null;
                NotifyChangedLevel();
                onLevelDestroyed.SafeInvoke();
            });
        }
    }

    public void LoadNextLevel(Action onLevelCreated)
    {
        DestroyCurrentLevel(() =>
        {
            _levelCreator.CreateLevel((level) =>
            {
                _currentLevel = level;
                NotifyChangedLevel();
                onLevelCreated.SafeInvoke();
            });
        });
    }

    private void NotifyChangedLevel()
    {
        _arguments.NewLevel = _currentLevel;
        TypeEventManager.Instance.Notify(_arguments, this);
    }
}

public struct LevelChangedEventArguments : IEventArguments
{
    public Level NewLevel;
}

