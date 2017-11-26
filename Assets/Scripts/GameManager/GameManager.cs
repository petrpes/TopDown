using Components.Common;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject _player;

    public GameObject PlayerPrefab { get { return _player; } }
    public GameObject PlayerInstance { get; set; }

    public GameManager()
    {

    }

    public void StartGame()
    {
        if (RoomManager.Instance != null && LevelManager.Instance != null)
        {
            LevelManager.Instance.LoadNextLevel(null);
        }
    }

    public void StopGame()
    {

    }
}
