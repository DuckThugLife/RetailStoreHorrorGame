using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private GameObject menuUICanvasPrefab;

    // Track spawned UI per player
    private Dictionary<ulong, List<GameObject>> playerUIMap = new();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetCursorState(bool unlocked)
    {
        Cursor.lockState = unlocked ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = unlocked;
    }

    public void EnableUICanvas(NetworkObject player, GameObject canvas)
    {
        if (canvas == null) return;

        canvas.SetActive(true);

        // Track it per-player
        if (!playerUIMap.ContainsKey(player.OwnerClientId))
            playerUIMap[player.OwnerClientId] = new List<GameObject>();

        if (!playerUIMap[player.OwnerClientId].Contains(canvas))
            playerUIMap[player.OwnerClientId].Add(canvas);
    }

    public void DisableUICanvas(NetworkObject player, GameObject canvas)
    {
        if (canvas == null) return;

        canvas.SetActive(false);

        if (playerUIMap.TryGetValue(player.OwnerClientId, out var list))
            list.Remove(canvas);
    }

    public void DisableAllUI(NetworkObject player)
    {
        if (!playerUIMap.TryGetValue(player.OwnerClientId, out var list)) return;

        foreach (var ui in list)
        {
            if (ui != null)
                ui.SetActive(false);
        }

        list.Clear();
    }

    // Optional: spawn the UI canvas and parent it to the player
    public GameObject SpawnMenuUI(NetworkObject player)
    {
        if (menuUICanvasPrefab == null) return null;

        GameObject canvas = Instantiate(menuUICanvasPrefab);
        canvas.SetActive(true);

        EnableUICanvas(player, canvas);

        return canvas;
    }
}
