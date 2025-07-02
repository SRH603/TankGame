using Unity.Netcode;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public void StartGame()
    {
        
    }

    public void EndGame()
    {
        
    }

    public void BackToTitle()
    {
        GameManager.instance.SetState(GameState.Title);
    }

    public void HostServer()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void ConnectToServer()
    {
        NetworkManager.Singleton.StartClient();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
