using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreListHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isVisible = false;
    [SerializeField] private bool _isEnabled = false;

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
        _isEnabled = false;
        _isVisible = false;
    }

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
        var show = _animator.GetBool("ShowFull");
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
        if (_isEnabled == false)
        {
            _isEnabled = true;
            _animator.SetTrigger("Show");
        }
    }

    private void Hide()
    {
        if (_isEnabled)
        {
            _isEnabled = false;
            _animator.SetTrigger("Hide");
        }
    }
}
