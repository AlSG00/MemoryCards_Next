using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreListHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private bool _isCursorOnMouse = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryShowList();
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
        _isCursorOnMouse = false;
    }

    private void ActivateList(bool setActive)
    {
        gameObject.GetComponent<BoxCollider>().enabled = setActive;
    }

    private void TryShowList()
    {
        if (_isCursorOnMouse == false)
        {
            Debug.Log("<color=yellow>Cursor's not on the list</color>");
            return;
        }
        var show = _animator.GetBool("Show");
        show = !show;
        _animator.SetBool("Show", show);
    }
}
