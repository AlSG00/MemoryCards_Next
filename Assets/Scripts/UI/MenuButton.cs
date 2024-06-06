using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public abstract class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private Animator _animator;

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        SetAnimatorValue(true);
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        SetAnimatorValue(false);
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        if (_animator is null)
        {
            throw new MissingComponentException("Animator component not found");
        }
    }

    private void SetAnimatorValue(bool value)
    {
        _animator.SetBool("MouseEnter", value);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnClickAction();
    }

    protected abstract void OnClickAction();
}
