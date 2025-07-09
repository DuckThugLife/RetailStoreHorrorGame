public interface IInteractables
{
    void OnFocus();
    void OnLoseFocus();
    void OnInteract();
    string GetInteractionPrompt();
}