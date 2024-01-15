using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Animator))]
public class Scale : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    public bool _isVisible;
    [SerializeField] private bool _isItemOnScale;

    private void Awake()
    {
        _isItemOnScale = false;
        _isVisible = false;
    }

    private void OnEnable()
    {
        ShopHandler.OnShowScale += ChangeVisibility;
    }

    private void OnDisable()
    {
        ShopHandler.OnShowScale -= ChangeVisibility;
    }

    private void ChangeVisibility(bool isActive)
    {
        if (isActive == _isVisible)
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

    private void Show()
    {
        if (_isVisible == false)
        {
            _isVisible = true;
            _animator.SetTrigger("Show");
        }
    }

    private void Hide()
    {
        if (_isVisible)
        {
            _isVisible = false;
            _animator.SetTrigger("Hide");
        }
    }


}
