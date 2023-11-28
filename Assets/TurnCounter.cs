using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCounter : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private bool _isVisible = false;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
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

    private void Break()
    {
        // TODO: Will trigger animation of breaking counter to disabling in till the next round
    }
}
