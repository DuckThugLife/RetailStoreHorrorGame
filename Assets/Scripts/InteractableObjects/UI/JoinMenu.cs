using UnityEngine;

public class JoinMenu : MonoBehaviour, IInteractables
{
    private IMenuHandler menuHandler;

    private void Awake()
    {
        menuHandler = GetComponent<IMenuHandler>();
        if (menuHandler == null)
            Debug.LogError("No component implementing IMenuHandler found on this GameObject!");
    }

    public void OnFocus(PlayerInteractor player)
    {
        player.ShowInteractCursor();
    }

    public void OnLoseFocus(PlayerInteractor player)
    {
        player.ShowDefaultCursor();
    }

    public void OnInteract()
    {
        Debug.Log("Menu interacted.");
        menuHandler?.OpenMenu();
    }

    public string GetInteractionPrompt()
    {
        return "Press E To Open Join Menu";
    }
}