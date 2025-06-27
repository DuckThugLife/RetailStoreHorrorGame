using UnityEngine;

public class GameplayState : IGameState
{
    public void Enter()
    {
        UIManager.Instance?.SetCursorState(false);
        Debug.Log("Entered Gameplay State");
    }

    public void Exit()
    {
        UIManager.Instance?.SetCursorState(true);
        Debug.Log("Exited Gameplay State");
    }
}