using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveItem : MonoBehaviour
{
    [SerializeField] private protected Animator _animator;
    [SerializeField] private protected float _moveToStandartPositionTime;
    [SerializeField] private protected float _moveToCursorTime;
    [SerializeField] private protected Transform _currentPivot;
    [SerializeField] private protected Transform _previousPivot;
    [SerializeField] private protected Transform _cursorPivot;
    private protected bool _isPicked;
    private protected bool _isReadyToUse;
    private protected bool _isChangingPosition;

    private protected void Update()
    {
        UpdateTransform();
    }

    private protected void UpdateTransform()
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

    private protected void SetReadyToUse(bool isReady)
    {
        _isReadyToUse = isReady;
    }

    private protected virtual void MoveToPivot(Transform target, float timeToMove)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToPivotRoutine(_currentPivot, timeToMove));
    }

    private protected virtual IEnumerator MoveToPivotRoutine(Transform targetTransform, float time = 0.2f)
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

    private protected virtual void OnMouseEnter() 
    {
        if (_animator == null)
        {
            throw new System.Exception($"Missing animator in {gameObject.name}");
        }

        _animator.SetBool("MouseOver", true);
    }

    private protected virtual void OnMouseExit() 
    {
        if (_animator == null)
        {
            throw new System.Exception($"Missing animator in {gameObject.name}");
        }

        _animator.SetBool("MouseOver", false);
    }

    private protected virtual void OnMouseDown() { }

    private protected virtual void OnMouseUp() { }
}
