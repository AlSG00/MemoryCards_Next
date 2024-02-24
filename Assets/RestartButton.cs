public class RestartButton : MenuButton
{
    public static event System.Action OnGameRestart;

    protected override void OnClickAction()
    {
        OnGameRestart?.Invoke();
    }
}
