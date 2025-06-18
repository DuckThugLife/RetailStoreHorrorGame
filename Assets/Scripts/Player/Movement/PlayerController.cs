using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;

    private CharacterController characterController;
    private PlayerStateMachine stateMachine;

    private InputAction moveAction;
    private Vector2 moveInput;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        stateMachine = new PlayerStateMachine();
    }

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        stateMachine.ChangeState(new WalkingState(this));
    }

    void Update()
    {
        moveInput = moveAction.ReadValue<Vector2>();
        stateMachine.Update();
    }

    public Vector2 GetMoveInput() => moveInput;
    public float GetMoveSpeed() => moveSpeed;
    public CharacterController GetController() => characterController;
}
