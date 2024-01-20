using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreListHandler : TableItem
{
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
       // _isEnabled = false;
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
}
