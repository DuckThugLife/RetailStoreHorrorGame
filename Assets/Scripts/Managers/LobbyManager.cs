using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager Instance { get; private set; }

    [SerializeField] private NetworkGameManager networkGameManager;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject lobbyUI;
    [SerializeField] private bool autoHostOnStart = true;

    private bool isLobbyMenuOpen = false;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        if (autoHostOnStart)
        {
            HideAllUI();
            ShowMainMenu(); // Or hide UI here if you want
        }
        else
        {
            ShowMainMenu();
        }
    }

    private IEnumerator DelayedHostStart()
    {
        yield return new WaitForSeconds(0.5f);
        networkGameManager.HostGame();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Lobby")
        {
            StartCoroutine(DelayedHostStart());
        }
    }

    private IEnumerator WaitForLocalPlayerAndSetState(IPlayerState state)
    {
        while (PlayerController.LocalPlayer == null)
        {
            yield return null;
        }

        PlayerController.LocalPlayer.StateMachine.ChangeState(state);
    }

    private void HideAllUI()
    {
        mainMenuUI.SetActive(false);
        lobbyUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isLobbyMenuOpen = false;
    }

    public void ToggleLobbyMenu()
    {
        if (isLobbyMenuOpen)
            CloseLobbyMenu();
        else
            OpenLobbyMenu();
    }

    public void OpenLobbyMenu()
    {
        lobbyUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        isLobbyMenuOpen = true;

        StartCoroutine(WaitForLocalPlayerAndSetState(new UIState(UIManager.Instance)));
    }

    public void CloseLobbyMenu()
    {
        lobbyUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        isLobbyMenuOpen = false;

        StartCoroutine(WaitForLocalPlayerAndSetState(new WalkingState(PlayerController.LocalPlayer)));
    }

    public void StartSoloLobby()
    {
        Debug.Log("Auto-hosting lobby for solo play...");
        networkGameManager.HostGame();
        ShowLobby();
    }

    public void ShowMainMenu()
    {
        mainMenuUI.SetActive(true);
        lobbyUI.SetActive(false);
    }

    public void ShowLobby()
    {
        mainMenuUI.SetActive(false);
        lobbyUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HostLobby()
    {
        networkGameManager.HostGame();
        ShowLobby();
    }

    public void JoinLobby()
    {
        networkGameManager.JoinGame();
        ShowLobby();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}