using UnityEngine;
using UnityEngine.InputSystem;

public class WalkingState : IPlayerState
{
    private readonly PlayerController player;
    private readonly Transform playerTransform;
    private readonly InputAction moveInput;
    private readonly float moveSpeed;

    public WalkingState(PlayerController player)
    {
        this.player = player;
        this.playerTransform = player.transform;
        this.moveInput = InputSystem.actions.FindAction("Move");
        this.moveSpeed = player.moveSpeed;

        if (moveInput == null)
        {
            Debug.LogError("Move action not found in InputSystem.actions");
        }
    }

    public void Enter() { }

    public void Exit() { }

    public void Update()
    {
        if (moveInput == null) return;

        Vector2 input = moveInput.ReadValue<Vector2>();
        Vector3 move = new Vector3(input.x, 0f, input.y);

        if (move.sqrMagnitude > 0.01f)
        {
            playerTransform.Translate(move * moveSpeed * Time.deltaTime, Space.Self);
        }
    }
}
