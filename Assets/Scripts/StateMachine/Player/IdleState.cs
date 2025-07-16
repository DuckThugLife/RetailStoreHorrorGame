using UnityEngine;
public class IdleState : IPlayerState
{
    private readonly PlayerController player;
    private readonly PlayerStateMachine stateMachine;

    public IdleState(PlayerController player, PlayerStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter() { }

    public void Update()
    {
        if (player.MoveInput != Vector2.zero)
        {
            stateMachine.ChangeState(new WalkingState(player));
        }
    }

    public void Exit() { }
}
