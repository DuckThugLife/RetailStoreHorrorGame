using UnityEngine;
public class LobbyState : IGameState
{
    public void Enter()
    {
        UIManager.Instance?.SetCursorState(false);
        Debug.Log("Entered Lobby State");
    }

    public void Exit()
    {
       
        Debug.Log("Exited Lobby State");
    }
}