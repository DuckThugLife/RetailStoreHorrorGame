public class PlayerStateMachine
{
    private IPlayerState currentState;

    public void ChangeState(IPlayerState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public IPlayerState GetCurrentState()
    {
        return currentState;
    }
}
