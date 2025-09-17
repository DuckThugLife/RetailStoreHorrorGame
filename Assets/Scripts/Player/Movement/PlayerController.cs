using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    [Header("Classes")]
    public static PlayerController LocalPlayer { get; private set; }
    public PlayerStateMachine StateMachine { get; private set; }
    public CharacterController Controller { get; private set; }

    public IMenuHandler MenuObjectHandler { get; private set; }

    [Header("Player Movement")]
    [field: SerializeField] public float MoveSpeed { get; private set; }
    public Vector2 MoveInput { get; private set; }

    private bool IsInOfflineMode =>
        NetworkManager.Singleton == null || !NetworkManager.Singleton.IsListening;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalPlayer = this;
        }
    }

    public override void OnNetworkDespawn()
    {
        if (IsOwner && LocalPlayer == this)
        {
            LocalPlayer = null;
        }
        base.OnNetworkDespawn();
    }

    private new void OnDestroy()
    {
        if (LocalPlayer == this)
        {
            LocalPlayer = null;
        }
    }

    private InputAction moveAction;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        StateMachine = new PlayerStateMachine();

        if (IsInOfflineMode)
        {
            LocalPlayer = this;
        }
    }

    private void Start()
    {
        Debug.Log("PlayerController started");
        moveAction = InputSystem.actions.FindAction("Move");
        StateMachine.ChangeState(new WalkingState(this));
        Debug.Log($"Changing player state to: - {StateMachine.GetCurrentState()}");
    }

    public void UpdateMenuHandler(IMenuHandler menu)
    { 
        MenuObjectHandler = menu;
    }


    private void Update()
    {
        // Allow movement if we're in offline mode OR we own the networked object
        if (!IsInOfflineMode && !IsOwner)
             return;

        MoveInput = moveAction.ReadValue<Vector2>();
        StateMachine.Update();
    }
}
