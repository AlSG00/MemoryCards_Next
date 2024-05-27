using UnityEngine;

public class ScoreListHandler : TableItem
{
    private bool _isFullyVisible;

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
        isVisible = false;
        _isFullyVisible = false;
    }

    private void OnMouseDown()
    {
        //isVisible = !isVisible;
        _isFullyVisible = !_isFullyVisible;
        ChangeFullListVisibility(_isFullyVisible);
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

        _isFullyVisible = isFullyVisible;
    }
}
