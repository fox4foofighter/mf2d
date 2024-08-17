using UnityEngine;
using Managers;

public class ManagerLoader : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        LoadManagers();

        Debug.Log("[Lifecycle] ManagerLoader awaked.");
    }

    private void LoadManagers()
    {
        new ConfigManager();
        Debug.Log("[Lifecycle] ConfigManager Loaded.");

        new DatabaseManager();
        Debug.Log("[Lifecycle] DatabaseManager Loaded.");
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
