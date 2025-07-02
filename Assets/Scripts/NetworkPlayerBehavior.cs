using TMPro;
using Unity.Netcode;
using UnityEngine;

public class NetworkPlayerBehavior : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            RegisterPlayerToLobbyListRpc(new RpcParams());
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
}
