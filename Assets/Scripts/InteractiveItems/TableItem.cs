using UnityEngine;

public abstract class TableItem : MonoBehaviour
{
    public bool isVisible;

    [SerializeField] private protected Animator _animator;

    private protected virtual void ChangeVisibility(bool setVisible)
    {
        if (setVisible == isVisible)
        {
            return;
        }

        if (setVisible)
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
        if (isVisible)
        {
            return;
        }

        isVisible = true;
        _animator.SetTrigger("Show");
    }

    private protected virtual void Hide()
    {
        if (isVisible == false)
        {
            return;
        }

        isVisible = false;
        _animator.SetTrigger("Hide");
    }
}
