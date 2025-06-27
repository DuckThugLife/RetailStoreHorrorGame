public class UIState : IGameState
{
    private UIManager uiManager;

    public UIState(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public void Enter()
    {
        uiManager.SetCursorState(true);
    }

    public void Exit()
    {
        uiManager.SetCursorState(false);
    }
}
