public class UIState : IPlayerState
{
    private UIManager uiManager;

    public UIState(UIManager uiManager)
    {
        this.uiManager = uiManager;
    }

    public void Enter()
    {
        uiManager.SetCursorState(true);
        // Show menu or pause UI
    }

    public void Exit()
    {
        uiManager.SetCursorState(false);
        // Hide menu or pause UI
    }

    public void Update()
    {
        // Optional: handle UI-specific input
    }
}
