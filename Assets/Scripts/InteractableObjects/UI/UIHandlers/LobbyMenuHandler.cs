using UnityEngine;

public class LobbyMenuHandler : MonoBehaviour, IMenuHandler
{
    public void OpenMenu(bool zoomCamera = true, bool disablePlayerControl = true)
    {
        // Use LobbyManager to open UI and manage cursor & player state
        LobbyManager.Instance.OpenLobbyMenu();

        if (zoomCamera)
        {
            // Your zoom camera logic here
            Debug.Log("Zooming camera in...");
        }

        if (disablePlayerControl)
        {
            // For example, disable player input here or via LobbyManager
            PlayerController.LocalPlayer?.StateMachine.ChangeState(new UIState(UIManager.Instance));
        }
    }

    public void CloseMenu()
    {
        LobbyManager.Instance.CloseLobbyMenu();

        // Re-enable player controls
        PlayerController.LocalPlayer?.StateMachine.ChangeState(new WalkingState(PlayerController.LocalPlayer));

        // Optionally reset camera zoom
        Debug.Log("Resetting camera zoom...");
    }
}
