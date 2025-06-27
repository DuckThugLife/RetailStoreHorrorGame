using UnityEngine;

public class MenuState : IGameState
{
    public void Enter()
    {
        UIManager.Instance?.SetCursorState(true);
        Debug.Log("Entered Menu State");
    }

    public void Exit()
    {
        UIManager.Instance?.SetCursorState(false);
        Debug.Log("Exited Menu State");
    }
}