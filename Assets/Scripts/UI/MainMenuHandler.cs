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
        BackToMenuButton.ReturningToMainMenu += ShowMenuDelayed;
    }

    private void OnDisable()
    {
        LeaveDifficultyMenuButton.LeavedDifficultyMenu -= ShowMenu;
        StartButton.StartPressed -= HideMenu;
        RejectStartButton.OnGameStartReject -= ShowMenu;
        SetLocaleButton.OnChooseLocale -= ShowMenu;
        NEW_GameProgression.FirstTimePlaying -= HideMenu;
        ScaleSuspend.OnSuspendGame -= ShowMenu;
        BackToMenuButton.ReturningToMainMenu -= ShowMenuDelayed;
    }

    private async void ShowMenuDelayed()
    {
        await System.Threading.Tasks.Task.Delay(2000);
        ShowMenu();
    }
}
