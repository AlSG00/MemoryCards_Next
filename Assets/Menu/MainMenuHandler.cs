public class MainMenuHandler : MenuHandler
{
    protected override void Awake()
    {
        ShowMenu();
    }

    private void OnEnable()
    {
        NEW_GameProgression.OnPressStart += HideMenu;
        RejectStartButton.OnGameStartReject += ShowMenu;
        SetLocaleButton.OnChooseLocale += ShowMenu;
        NEW_GameProgression.FirstTimePlaying += HideMenu;
        ScaleSuspend.OnSuspendGame += ShowMenu;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnPressStart -= HideMenu;
        RejectStartButton.OnGameStartReject -= ShowMenu;
        SetLocaleButton.OnChooseLocale -= ShowMenu;
        NEW_GameProgression.FirstTimePlaying -= HideMenu;
        ScaleSuspend.OnSuspendGame -= ShowMenu;
    }
}
