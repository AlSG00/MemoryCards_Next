public class PauseMenuHandler : MenuHandler
{
    private void OnEnable()
    {
        NEW_GameProgression.PauseGame += SetMenuVisibility;
        BackToMenuButton.ReturningToMainMenu += HideMenu;
    }

    private void OnDisable()
    {
        NEW_GameProgression.PauseGame -= SetMenuVisibility;
        BackToMenuButton.ReturningToMainMenu -= HideMenu;
    }
}
