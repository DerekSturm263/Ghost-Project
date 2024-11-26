using Unity.Netcode;
using UnityEngine;

public class HelloWorldPlayer : NetworkBehaviour
{
    public NetworkVariable<Vector3> position = new();

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Move();
        }
    }

    public void Move()
    {
        //SubmitPositionRequestRpc();
    }

    /*[Rpc(SendTo.Server)]
    void SubmitPositionRequestRpc(RpcParams rpcParams = default)
    {
        var randomPosition = GetRandomPositionOnPlane();

        transform.position = randomPosition;
        position.Value = randomPosition;
    }*/

    public static Vector3 GetRandomPositionOnPlane() => new(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));

    private void Update()
    {
        transform.position = position.Value;
    }
}
