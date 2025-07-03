using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NetworkPlayerBehavior : NetworkBehaviour
{
    [SerializeField] GameObject networkPlayerTankTemplate;
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            RegisterPlayerToLobbyListRpc(new RpcParams());
            NetworkManager.Singleton.SceneManager.OnSceneEvent += SceneManager_onSceneEvent;
        }
    }

    [Rpc(SendTo.Server)]
    private void RegisterPlayerToLobbyListRpc(RpcParams rpcParams)
    {
        GameObject.Find("PlayerListText").GetComponent<TMP_Text>().text += "Player" + rpcParams.Receive.SenderClientId;
        UpdatePlayerListRpc(GameObject.Find("PlayerListText").GetComponent<TMP_Text>().text);
    }
    
    [Rpc(SendTo.ClientsAndHost)]
    private void UpdatePlayerListRpc(string newListText)
    {
        GameObject.Find("PlayerListText").GetComponent<TMP_Text>().text = newListText;
    }

    [Rpc(SendTo.Server)]
    private void SpawnInNetworkedPlayerTankRpc(RpcParams rpcParams)
    {
        GameObject newTank = Instantiate(networkPlayerTankTemplate);
        newTank.GetComponent<NetworkObject>().SpawnWithOwnership(rpcParams.Receive.SenderClientId);
    }

    private void SceneManager_onSceneEvent(SceneEvent sceneEvent)
    {
        if (sceneEvent.SceneEventType == SceneEventType.LoadComplete)
        {
            if (SceneManager.GetActiveScene().name == "4_NetworkGame")
            {
                if (IsOwner)
                {
                    SpawnInNetworkedPlayerTankRpc(new RpcParams());
                }
            }
                
        }
    }
    
}
