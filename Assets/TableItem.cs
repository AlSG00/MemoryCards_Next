using UnityEngine;

public abstract class TableItem : MonoBehaviour
{
    public bool isVisible;
    [SerializeField] protected Animator _animator;

    private protected virtual void ChangeVisibility(bool isActive)
    {
        if (isActive == isVisible)
        {
            return;
        }

        if (isActive)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private protected virtual void Show()
    {
        if (isVisible == false)
        {
            isVisible = true;
            _animator.SetTrigger("Show");
        }
    }

    private protected virtual void Hide()
    {
        if (isVisible)
        {
            isVisible = false;
            _animator.SetTrigger("Hide");
        }
    }
}
