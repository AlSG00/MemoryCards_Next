using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractiveItem : MonoBehaviour
{
    [SerializeField] private protected InteractiveItemAudioPlayer _audioPlayer;
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

        if (_isPicked == false)
        {
            return;
        }

        if (_currentPivot == null)
        {
            return;
        }

        transform.position = _currentPivot.position;
        transform.rotation = _currentPivot.rotation;
    }

    private protected void SetReadyToUse(bool isReady)
    {
        _isReadyToUse = isReady;
    }

    private protected virtual void MoveToPivot(Transform target, float timeToMove)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToPivotRoutine(target, timeToMove));
    }

    private protected virtual void MoveToPositionWithOffset(Transform target, Vector3 offset, float timeToMove)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToPositionWithOffsetRoutine(target, offset, timeToMove));
    }

    private protected IEnumerator MoveToPivotRoutine(Transform targetTransform, float time = 0.2f)
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

        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;

        _isChangingPosition = false;
    }

    private protected IEnumerator MoveToPositionWithOffsetRoutine(Transform targetTransform, Vector3 offset, float time = 0.2f)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = new Vector3(
            targetTransform.position.x + offset.x,
            targetTransform.position.y + offset.y,
            targetTransform.position.z + offset.z
            );

        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        _isChangingPosition = false;
    }

    private protected virtual void OnMouseEnter()
    {
        if (_animator == null)
        {
            return;
        }
        _audioPlayer?.MouseEnter();
        _animator.SetBool("MouseOver", true);
    }

    private protected virtual void OnMouseExit()
    {
        if (_animator == null)
        {
            return;
        }
        _audioPlayer?.MouseExit();
        _animator.SetBool("MouseOver", false);
    }

    private protected virtual void OnMouseDown() { }

    private protected virtual void OnMouseUp() { }
}
