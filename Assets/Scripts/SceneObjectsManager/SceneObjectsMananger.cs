public class SceneObjectsMananger
{
    public static SceneObjectsMananger Instance = new SceneObjectsMananger();

    public readonly ISpawnManager SpawnManager;
    public readonly IObjectAppearanceHooks<object> AppearanceHooks;

    public SceneObjectsMananger()
    {
        SpawnManager = new SpawnManager();
        AppearanceHooks = new ObjectAppearanceHooks();
    }
}

