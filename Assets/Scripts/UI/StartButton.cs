public class StartButton : MenuButton
{
    public static event System.Action StartPressed;

    protected override void OnClickAction()
    {
        StartPressed?.Invoke();
    }
}
