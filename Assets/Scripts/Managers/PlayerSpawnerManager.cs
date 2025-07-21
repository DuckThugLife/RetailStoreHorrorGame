using Unity.Netcode;
using UnityEngine;

public class PlayerSpawnerManager : MonoBehaviour
{
    public static PlayerSpawnerManager Instance { get; private set; }

    [SerializeField] private GameObject playerPrefab;

    private GameObject offlinePlayerInstance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        if (IsInOfflineMode)
        {
            SpawnLocalPlayer();
        }
        else
        {
            if (NetworkManager.Singleton != null)
            {
                NetworkManager.Singleton.OnServerStarted += OnServerStarted;
            }
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private bool IsInOfflineMode =>
        NetworkManager.Singleton == null || !NetworkManager.Singleton.IsListening;

    private void OnServerStarted()
    {
        SubscribeToClientConnect();
    }

    private void SubscribeToClientConnect()
    {
        if (NetworkManager.Singleton != null)
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        }
    }

    private void OnClientConnected(ulong clientId)
    {
        if (NetworkManager.Singleton.IsServer)
            SpawnPlayer(clientId);
    }

    private void SpawnPlayer(ulong clientId)
    {
        var playerInstance = Instantiate(playerPrefab);
        var networkObject = playerInstance.GetComponent<NetworkObject>();
        networkObject.SpawnAsPlayerObject(clientId, true);
    }

    private void SpawnLocalPlayer()
    {
        offlinePlayerInstance = Instantiate(playerPrefab);
        Debug.Log("Spawned offline-mode player");
    }

    public void CleanupOfflinePlayer()
    {
        if (offlinePlayerInstance != null)
        {
            Destroy(offlinePlayerInstance);
            offlinePlayerInstance = null;
            Debug.Log("Destroyed offline-mode player manually.");
        }
    }
}
