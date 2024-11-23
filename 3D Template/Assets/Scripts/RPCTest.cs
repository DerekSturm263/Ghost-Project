using Unity.Netcode;
using UnityEngine;

public class RPCTest : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        // Only send an RPC to the server from the client that owns the NetworkObject of this NetworkBehaviour instance.
        if (!IsServer && IsOwner)
        {
            //ServerOnlyRpc(0, NetworkObjectId);
        }
    }

    /*[Rpc(SendTo.ClientsAndHost)]
    void ClientAndHostRpc(int value, ulong sourceNetworkObjectId)
    {
        Debug.Log($"Client Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");

        // Only send an RPC to the owner of the NetworkObject.
        if (IsOwner)
            ServerOnlyRpc(value + 1, sourceNetworkObjectId);
    }*/

    /*[Rpc(SendTo.Server)]
    void ServerOnlyRpc(int value, ulong sourceNetworkObjectId)
    {
        Debug.Log($"Server Received the RPC #{value} on NetworkObject #{sourceNetworkObjectId}");

        ClientAndHostRpc(value, sourceNetworkObjectId);
    }*/
}
