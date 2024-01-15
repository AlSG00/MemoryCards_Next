using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreListHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    //private bool _isCursorOnMouse = false;
    [SerializeField] private bool _isVisible = false;
    [SerializeField] private bool isEnabled = false;

    private void OnEnable()
    {
        NEW_GameProgression.OnActivateScoreList += SetScoreListActive;
        NEW_Card.OnHideFullList += ChangeFullListVisibility;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnActivateScoreList -= SetScoreListActive;
        NEW_Card.OnHideFullList -= ChangeFullListVisibility;
    }

    private void Awake()
    {
        isEnabled = false;
        _isVisible = false;
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        ChangeFullListVisibility();
    //    }
    //}

    //private void OnMouseEnter()
    //{
    //    _isCursorOnMouse = true;
    //}

    //private void OnMouseOver()
    //{
    //    _isCursorOnMouse = true;
    //}

    //private void OnMouseExit()
    //{
    //    _isCursorOnMouse = false;
    //}

    private void OnMouseDown()
    {
        _isVisible = !_isVisible;
        ChangeFullListVisibility(_isVisible);
    }

    private void ActivateList(bool setActive)
    {
        gameObject.GetComponent<BoxCollider>().enabled = setActive;
    }

    private void ChangeFullListVisibility(bool isFullyVisible)
    {
        //if (_isCursorOnMouse == false)
        //{
        //    return;
        //}

        var show = _animator.GetBool("ShowFull");
        //show = !show;
        if (show != isFullyVisible)
        {
            _animator.SetBool("ShowFull", isFullyVisible);
        }

        _isVisible = isFullyVisible; // Needed for syncing values when invoking metod from other scripts;
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
        if (isEnabled == false)
        {
            isEnabled = true;
            _animator.SetTrigger("Show");
        }
    }

    private void Hide()
    {
        if (isEnabled)
        {
            isEnabled = false;
            _animator.SetTrigger("Hide");
        }
    }
}
