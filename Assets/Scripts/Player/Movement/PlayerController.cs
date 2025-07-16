using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{

    [Header("Classes")]
    public static PlayerController LocalPlayer { get; private set; }
    public PlayerStateMachine StateMachine { get; private set; }
    public CharacterController Controller { get; private set; }

    [Header("Player Movement")]
    [field: SerializeField] public float MoveSpeed { get; private set; }
    public Vector2 MoveInput { get; private set; }

   
   

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            LocalPlayer = this;
        }
    }
    

    private InputAction moveAction;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        StateMachine = new PlayerStateMachine();
    }

    private void Start()
    {
        
        moveAction = InputSystem.actions.FindAction("Move");
        StateMachine.ChangeState(new WalkingState(this));
        Debug.Log($"Changing player state to: - {StateMachine.GetCurrentState()}");
    }

    private void Update()
    {
        if (!LocalPlayer) return;  //  Prevent input/movement for non-local players

        MoveInput = moveAction.ReadValue<Vector2>();
        StateMachine.Update();
    }
}
