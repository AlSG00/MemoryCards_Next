public class RejectStartButton : MenuButton
{
    public static event System.Action OnGameStartReject;

    protected override void OnClickAction()
    {
        OnGameStartReject?.Invoke();
    }
}
