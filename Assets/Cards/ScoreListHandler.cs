using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreListHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private bool _isCursorOnMouse = false;
    private bool _isVisible = false;

    private void OnEnable()
    {
        NEW_GameProgression.OnActivateScoreList += SetScoreListActive;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnActivateScoreList -= SetScoreListActive;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ChangeFullListVisibility();
        }
    }

    private void OnMouseEnter()
    {
        _isCursorOnMouse = true;
    }

    private void OnMouseOver()
    {
        _isCursorOnMouse = true;
    }

    private void OnMouseExit()
    {
        //ChangeListVisibility();
        _isCursorOnMouse = false;
    }

    private void ActivateList(bool setActive)
    {
        gameObject.GetComponent<BoxCollider>().enabled = setActive;
    }

    private void ChangeFullListVisibility()
    {
        if (_isCursorOnMouse == false)
        {
            return;
        }

        var show = _animator.GetBool("ShowFull");
        show = !show;
        _animator.SetBool("ShowFull", show);
    }

    private void SetScoreListActive(bool isActive)
    {
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
