public interface IMenuHandler
{
    void OpenMenu(bool zoomCamera = false, bool disablePlayerControl = false);
    void CloseMenu();
}