using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleItemContinue : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private bool _isPicked;
    [SerializeField] private bool _isReadyToUse;

    private void Awake()
    {
        _isPicked = false;
        _isReadyToUse = false;
    }

    private void OnEnable()
    {
        ScaleColliderHandler.OnEnterCollider += SetReadyToUse;
    }

    private void OnDisable()
    {
        ScaleColliderHandler.OnEnterCollider -= SetReadyToUse;
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        
    }

    private void OnMouseDown()
    {
        _isPicked = true;
    }

    private void OnMouseUp()
    {
        _isPicked = false;
        if (_isReadyToUse)
        {

        }
        else
        {

        }
    }

    private void SetReadyToUse(bool isReady)
    {
        _isReadyToUse = isReady;
    }
}
