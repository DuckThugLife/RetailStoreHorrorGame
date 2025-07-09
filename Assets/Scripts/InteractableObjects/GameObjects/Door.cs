using UnityEngine;

public class Door : MonoBehaviour, IInteractables
{
    private bool isOpen = false;

    public void OnFocus()
    {
        Debug.Log("Door is highlighted or showing a prompt.");
    }

    public void OnLoseFocus()
    {
        Debug.Log("Stopped highlighting door.");
    }

    public void OnInteract()
    {
        isOpen = !isOpen;
        Debug.Log(isOpen ? "Door opened." : "Door closed.");
    }

    public string GetInteractionPrompt()
    {
        return isOpen ? "Press E to close the door" : "Press E to open the door";
    }
}
