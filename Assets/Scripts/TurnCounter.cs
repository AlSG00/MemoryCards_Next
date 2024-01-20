using UnityEngine;

public class TurnCounter : TableItem
{
    public static System.Action<bool> OnActivateTurnCounter;

    private void OnEnable()
    {
        NEW_GameProgression.OnActivateTurnCounter += ChangeVisibility;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnActivateTurnCounter -= ChangeVisibility;
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

    private void Break()
    {
        // TODO: Will trigger animation of breaking counter to disable it 'till the next round
    }
}
