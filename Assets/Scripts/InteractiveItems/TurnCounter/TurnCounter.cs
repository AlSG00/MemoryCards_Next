public class TurnCounter : TableItem
{
    public static System.Action<bool> OnActivateTurnCounter;

    private void OnEnable()
    {
        GameProgression.OnActivateTurnCounter += ChangeVisibility;
        ScrewdriverUseLogic.OnUseScrewdriver += DeactivateByScrewdriver;
    }

    private void OnDisable()
    {
        GameProgression.OnActivateTurnCounter -= ChangeVisibility;
        ScrewdriverUseLogic.OnUseScrewdriver += DeactivateByScrewdriver;
    }

    private protected override void Show()
    {
        if (isVisible == false)
        {
            isVisible = true;
            _animator.SetTrigger("Show");
            OnActivateTurnCounter?.Invoke(true);
        }
    }

    private protected override void Hide()
    {
        if (isVisible)
        {
            isVisible = false;
            _animator.SetTrigger("Hide");
            OnActivateTurnCounter?.Invoke(false);
        }
    }

    private void DeactivateByScrewdriver()
    {
        if (isVisible == false)
        {
            return;
        }

        isVisible = false;
        _animator.SetTrigger("Deactivate");
        OnActivateTurnCounter?.Invoke(false);
    }
}
