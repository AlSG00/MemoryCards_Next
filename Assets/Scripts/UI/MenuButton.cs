using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Animator))]
public class MenuButton : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnMouseDown()
    {
        GetComponent<IButtonAction>().OnClickAction();
    }

    private void OnMouseEnter()
    {
        SetAnimatorValue(true);
    }

    private void OnMouseExit()
    {
        SetAnimatorValue(false);
    }

    private void SetAnimatorValue(bool value)
    {
        _animator.SetBool("MouseEnter", value);
    }
}
