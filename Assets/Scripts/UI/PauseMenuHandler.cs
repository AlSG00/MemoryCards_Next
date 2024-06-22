public class PauseMenuHandler : MenuHandler
{
    private void OnEnable()
    {
        GameProgression.PauseGame += SetMenuVisibility;
        BackToMenuButton.ReturningToMainMenu += HideMenu;
    }

    private void OnDisable()
    {
        GameProgression.PauseGame -= SetMenuVisibility;
        BackToMenuButton.ReturningToMainMenu -= HideMenu;
    }
}
