using Unity.Netcode;
using UnityEngine;

public class PlayerSpawnerManager : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private void OnEnable()
    {
        if (!IsServer) return;
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
    }

    private void OnDisable()
    {
        if (NetworkManager.Singleton != null && IsServer)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        SpawnPlayer(clientId);
    }

    private void SpawnPlayer(ulong clientId)
    {
        var playerInstance = Instantiate(playerPrefab);
        var networkObject = playerInstance.GetComponent<NetworkObject>();
        networkObject.SpawnAsPlayerObject(clientId, true);
    }
}