public class GameManager
{
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
