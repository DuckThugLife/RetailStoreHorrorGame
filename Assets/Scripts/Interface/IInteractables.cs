public interface IInteractables
{
    void OnFocus(PlayerInteractor playerInteractor);
    void OnLoseFocus(PlayerInteractor playerInteractor);
    void OnInteract();
    string GetInteractionPrompt();
}