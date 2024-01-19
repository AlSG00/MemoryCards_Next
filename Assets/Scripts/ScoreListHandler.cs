using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreListHandler : TableItem
{
    //[SerializeField] private Animator _animator;
    //[SerializeField] private bool _isVisible = false;
    [SerializeField] private bool _isEnabled = false;

    private void OnEnable()
    {
        NEW_GameProgression.OnActivateScoreList += ChangeVisibility;
        NEW_Card.OnHideFullList += ChangeFullListVisibility;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnActivateScoreList -= ChangeVisibility;
        NEW_Card.OnHideFullList -= ChangeFullListVisibility;
    }

    private void Awake()
    {
        _isEnabled = false;
        isVisible = false;
    }

    private void OnMouseDown()
    {
        isVisible = !isVisible;
        ChangeFullListVisibility(isVisible);
    }

    private void ActivateList(bool setActive)
    {
        gameObject.GetComponent<BoxCollider>().enabled = setActive;
    }

    private void ChangeFullListVisibility(bool isFullyVisible)
    {
        var show = _animator.GetBool("ShowFull");
        if (show != isFullyVisible)
        {
            _animator.SetBool("ShowFull", isFullyVisible);
        }

        isVisible = isFullyVisible; // Needed for syncing values when invoking metod from other scripts;
    }

    //private void ChangeVisibility(bool isActive)
    //{
    //    if (isActive)
    //    {
    //        Show();
    //    }
    //    else
    //    {
    //        Hide();
    //    }
    //}

    //private void Show()
    //{
    //    if (_isEnabled == false)
    //    {
    //        _isEnabled = true;
    //        _animator.SetTrigger("Show");
    //    }
    //}

    //private void Hide()
    //{
    //    if (_isEnabled)
    //    {
    //        _isEnabled = false;
    //        _animator.SetTrigger("Hide");
    //    }
    //}
}
