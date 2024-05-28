public class LeaveDifficultyMenuButton : MenuButton
{
    public static event System.Action LeavedDifficultyMenu;

    protected override void OnClickAction()
    {
        LeavedDifficultyMenu?.Invoke();
    }
}
