using UnityEngine;

public class WalkingState : IPlayerState
{
    private readonly PlayerController player;
    private readonly PlayerStateMachine stateMachine;

    public WalkingState(PlayerController player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter() { }

    public void Update()
    {
        Vector2 input = player.GetMoveInput();
        if (input == Vector2.zero)
        {
            stateMachine.ChangeState(new IdleState(player, stateMachine));
            return;
        }

        Vector3 move = new Vector3(input.x, 0, input.y);
        player.transform.Translate(move * player.GetMoveSpeed() * Time.deltaTime, Space.World);

        // Optional: Rotate player to face direction
        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    public void Exit() { }
}
