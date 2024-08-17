using UnityEngine;
using Managers;

public class GameContainer : MonoBehaviour
{
    public static GameContainer _;


    private void Awake()
    {
        _ = this;
        DontDestroyOnLoad(gameObject);

        LoadManagers();

        Debug.Log("[Lifecycle] ManagerLoader awaked.");
    }

    private void LoadManagers()
    {
        Debug.Log("[Lifecycle] ManagerLoader started.");

        new ConfigManager();
        Debug.Log("[Lifecycle] ConfigManager loaded.");

        new DatabaseManager();
        Debug.Log("[Lifecycle] DatabaseManager loaded.");

        new InputManager();
        Debug.Log("[Lifecycle] InputManager loaded.");

        new ControlManager();
        Debug.Log("[Lifecycle] ControlManager loaded.");
    }

    private void Update()
    {
        // パイプライン式にしてもいいかも
        InputManager._?.Update();
        ControlManager._?.Update();
    }

    private void OnDestroy()
    {
        DatabaseManager._?.Destroy();
    }

    private void OnApplicationQuit()
    {
        DatabaseManager._?.Destroy();
    }
}
