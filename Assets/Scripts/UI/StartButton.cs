public class StartButton : MenuButton
{
    public static event System.Action OnGameStart;

    protected override void OnClickAction()
    {
        OnGameStart?.Invoke();
    }
}
