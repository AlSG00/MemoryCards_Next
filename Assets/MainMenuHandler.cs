public class MainMenuHandler : MenuHandler
{
    protected override void Awake()
    {
        ShowMenu();
    }

    private void OnEnable()
    {
        LeaveDifficultyMenuButton.LeavedDifficultyMenu += ShowMenu;
        StartButton.StartPressed += HideMenu;
        RejectStartButton.OnGameStartReject += ShowMenu;
        SetLocaleButton.OnChooseLocale += ShowMenu;
        NEW_GameProgression.FirstTimePlaying += HideMenu;
        ScaleSuspend.OnSuspendGame += ShowMenu;
    }

    private void OnDisable()
    {
        LeaveDifficultyMenuButton.LeavedDifficultyMenu -= ShowMenu;
        StartButton.StartPressed -= HideMenu;
        RejectStartButton.OnGameStartReject -= ShowMenu;
        SetLocaleButton.OnChooseLocale -= ShowMenu;
        NEW_GameProgression.FirstTimePlaying -= HideMenu;
        ScaleSuspend.OnSuspendGame -= ShowMenu;
    }
}
