using UnityEngine;

public class LobbyMenuHandler : MonoBehaviour, IMenuHandler
{
    public void OpenMenu(bool zoomCamera = true, bool disablePlayerControl = true)
    {
        // Use LobbyManager to open UI and manage cursor & player state
        PlayerController.LocalPlayer?.UpdateMenuHandler(this); // Making sure the player can keep track of the object he has opened
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
        PlayerController.LocalPlayer?.UpdateMenuHandler(null); // Getting rid of the menu on the player after they close it
        // Re-enable player controls
        PlayerController.LocalPlayer?.StateMachine.ChangeState(new WalkingState(PlayerController.LocalPlayer));
        
        // Optionally reset camera zoom
        Debug.Log("Resetting camera zoom...");
    }
}
