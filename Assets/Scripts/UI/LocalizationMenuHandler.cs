public class LocalizationMenuHandler : MenuHandler
{
    private void OnEnable()
    {
        GameProgression.FirstTimePlaying += ShowMenu;
        SetLocaleButton.OnChooseLocale += HideMenu;
    }

    private void OnDisable()
    {
        GameProgression.FirstTimePlaying -= ShowMenu;
        SetLocaleButton.OnChooseLocale += HideMenu;
    }
}
