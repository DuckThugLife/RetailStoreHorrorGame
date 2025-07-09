using UnityEngine;
public class GameState : IGameState
{
    public void Enter()
    {
        UIManager.Instance?.SetCursorState(false);
        Debug.Log("Entered Game State");
    }

    public void Exit()
    {
       
        Debug.Log("Exited Game State");
    }
}