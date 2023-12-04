using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private Transform _currentPivot;
    [SerializeField] private Transform _shopPivot;
    [SerializeField] private Transform _inventoryPivot;
    [SerializeField] private Transform _cursorPivot;

    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isReadyToUse;
    [SerializeField] private bool _isPicked;
    [SerializeField] private bool _isChangingPosition;
    
    public static event System.Action OnTakeItem;

    [SerializeField] private float _moveToInventoryTime;
    [SerializeField] private float _moveToCursorTime;

    private void Awake()
    {
        _isReadyToUse = false;
        _isPicked = false;
        _isChangingPosition = false;
    }

    private void Update()
    {
        UpdateTransform();
    }

    private void UpdateTransform()
    {
        if (_isChangingPosition)
        {
            return;
        }

        if (_currentPivot != null)
        {
            transform.position = _currentPivot.position;
            transform.rotation = _currentPivot.rotation;
        }
    }

    private void OnMouseEnter()
    {
        Debug.Log("Entered");
    }

    private void OnMouseExit()
    {
        Debug.Log("Leaved");
    }

    private void OnMouseDown()
    {
        _isChangingPosition = true;
        _currentPivot = _cursorPivot;
        StopAllCoroutines();
        StartCoroutine(MoveToPivotRoutine(_currentPivot, _moveToCursorTime));
    }

    private async void OnMouseUp()
    {
        _isChangingPosition = true;
        _currentPivot = _inventoryPivot;
        StopAllCoroutines();
        StartCoroutine(MoveToPivotRoutine(_currentPivot, _moveToInventoryTime));
    }

    private IEnumerator MoveToPivotRoutine(Transform targetTransform, float time)
    {
        Vector3 startPosition = transform.position;
        Quaternion startRotation = transform.rotation;
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startPosition, targetTransform.position, (elapsedTime / time));
            transform.rotation = Quaternion.Lerp(startRotation, targetTransform.rotation, (elapsedTime / time));
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        _isChangingPosition = false;
    }
}
