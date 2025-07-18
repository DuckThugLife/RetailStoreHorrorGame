using Unity.Netcode;
using UnityEngine;

public class PlayerSpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private bool subscribed = false;

    private void Start()
    {
        if (IsInOfflineMode)
        {
            // Offline: just spawn local player immediately
            SpawnLocalPlayer();
        }
        else
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.OnServerStarted += OnServerStarted;

                if (NetworkManager.Singleton.IsServer)
                    SubscribeToClientConnect();
            }
        }
    }

    private void OnDestroy()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnServerStarted -= OnServerStarted;
            UnsubscribeFromClientConnect();
        }
    }

    private bool IsInOfflineMode =>
        NetworkManager.Singleton == null || !NetworkManager.Singleton.IsListening;

    private void OnServerStarted()
    {
        SubscribeToClientConnect();
    }

    private void SubscribeToClientConnect()
    {
        if (!subscribed)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            subscribed = true;
            Debug.Log("Subscribed to OnClientConnectedCallback");
        }
    }

    private void UnsubscribeFromClientConnect()
    {
        if (subscribed)
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
            subscribed = false;
            Debug.Log("Unsubscribed from OnClientConnectedCallback");
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        Debug.Log($"Client connected: {clientId}");
        SpawnPlayer(clientId);
    }

    private void SpawnPlayer(ulong clientId)
    {
        if (!NetworkManager.Singleton.IsServer)
        {
            Debug.LogWarning("SpawnPlayer called on non-server!");
            return;
        }

        var playerInstance = Instantiate(playerPrefab);
        var networkObject = playerInstance.GetComponent<NetworkObject>();

        if (networkObject == null)
        {
            Debug.LogError("Player prefab missing NetworkObject component!");
            return;
        }

        networkObject.SpawnAsPlayerObject(clientId, true);
        Debug.Log($"Spawned player for client {clientId}");
    }

    private void SpawnLocalPlayer()
    {
        // Offline mode: just instantiate player and set local static ref
        var playerInstance = Instantiate(playerPrefab);
        var playerController = playerInstance.GetComponent<PlayerController>();
       
        Debug.Log("Spawned local player for offline mode");
    }
}
