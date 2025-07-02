using UnityEngine;
using Unity.Netcode;

public class NetworkStarter : MonoBehaviour
{
    public void StartHost()
    {
        if (!NetworkManager.Singleton.IsListening)
        {
            bool result = NetworkManager.Singleton.StartHost();
            Debug.Log($"StartHost result: {result}");
        }
        else
        {
            Debug.LogWarning("Network already started.");
        }
    }

    public void StartClient()
    {
        if (!NetworkManager.Singleton.IsListening)
        {
            bool result = NetworkManager.Singleton.StartClient();
            Debug.Log($"StartClient result: {result}");
        }
    }

    public void StartServer()
    {
        if (!NetworkManager.Singleton.IsListening)
        {
            bool result = NetworkManager.Singleton.StartServer();
            Debug.Log($"StartServer result: {result}");
        }
    }
}