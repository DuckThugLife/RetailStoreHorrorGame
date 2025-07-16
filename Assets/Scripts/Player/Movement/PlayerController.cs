using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    [field: SerializeField] public float MoveSpeed { get; private set; }

    public CharacterController Controller { get; private set; }
    public Vector2 MoveInput { get; private set; }

    private PlayerStateMachine stateMachine;
    private InputAction moveAction;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        stateMachine = new PlayerStateMachine();
    }

    private void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        stateMachine.ChangeState(new WalkingState(this));
    }

    private void Update()
    {
        if (!IsOwner) return;  //  Prevent input/movement for non-local players

        MoveInput = moveAction.ReadValue<Vector2>();
        stateMachine.Update();
    }
}
