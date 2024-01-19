using UnityEngine;

public class TurnCounter : TableItem
{
    //[SerializeField] private Animator _animator;

    private bool _isVisible = false;

    public static System.Action<bool> OnActivateTurnCounter;

    private void OnEnable()
    {
        NEW_GameProgression.OnActivateTurnCounter += ChangeVisibility;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnActivateTurnCounter -= ChangeVisibility;
    }

    //private void SetTurnCounterActive(bool isActive)
    //{
    //    if (isActive == _isVisible)
    //    {
    //        return;
    //    }

    //    if (isActive)
    //    {
    //        Show();
    //    }
    //    else
    //    {
    //        Hide();
    //    }
    //}

    private protected override void Show()
    {
        if (_isVisible == false)
        {
            _isVisible = true;
            _animator.SetTrigger("Show");
            OnActivateTurnCounter?.Invoke(true);
        }
    }

    private protected override void Hide()
    {
        if (_isVisible)
        {
            _isVisible = false;
            _animator.SetTrigger("Hide");
            OnActivateTurnCounter?.Invoke(false);
        }
    }

    private void Break()
    {
        // TODO: Will trigger animation of breaking counter to disable it 'till the next round
    }
}
