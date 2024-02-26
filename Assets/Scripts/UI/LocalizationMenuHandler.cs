public class LocalizationMenuHandler : MenuHandler
{
    private void OnEnable()
    {
        NEW_GameProgression.FirstTimePlaying += ShowMenu;
        SetLocaleButton.OnChooseLocale += HideMenu;
    }

    private void OnDisable()
    {
        NEW_GameProgression.FirstTimePlaying -= ShowMenu;
        SetLocaleButton.OnChooseLocale += HideMenu;
    }
}
