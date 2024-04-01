using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleItem : InteractiveItem
{
    [SerializeField] private Transform _selfPivot;
    [SerializeField] private bool _isVisible;

    private void Awake()
    { 
        _isPicked = false;
        _isReadyToUse = false;
        _isVisible = false;
        GetComponent<Collider>().enabled = false;
    }

    private void OnEnable()
    {
        ScaleColliderHandler.OnEnterCollider += SetReadyToUse;
        ShopHandler.OnShowScaleItems += ChangeVisibility;
    }

    private void OnDisable()
    {
        ScaleColliderHandler.OnEnterCollider -= SetReadyToUse;
        ShopHandler.OnShowScaleItems -= ChangeVisibility;
    }

    private protected override void OnMouseDown()
    {
        _isChangingPosition = true;
        _currentPivot = _cursorPivot;
        GetComponent<Collider>().enabled = false;
        MoveToPivot(_currentPivot, _moveToCursorTime);
        _isPicked = true;
    }

    private protected override void OnMouseUp()
    {
        _isPicked = false;
        if (_isReadyToUse)
        {
            _isReadyToUse = false;
            GetComponent<IUsable>().Use();
        }

        _isChangingPosition = true;
        Cursor.visible = true;
        _currentPivot = _selfPivot;
        GetComponent<Collider>().enabled = true;
        MoveToPivot(_currentPivot, _moveToStandartPositionTime);
        _isPicked = false;
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

        GetComponent<Collider>().enabled = true;
    }

    private void Hide()
    {
        if (_isVisible)
        {
            _isVisible = false;
            _animator.SetTrigger("Hide");
        }

        GetComponent<Collider>().enabled = false;
    }
}
