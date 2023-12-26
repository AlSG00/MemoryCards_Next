using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleItem : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [SerializeField] private bool _isPicked;
    [SerializeField] private bool _isReadyToUse;

    [SerializeField] private Transform _currentPivot;
    [SerializeField] private Transform _previousPivot;
    [SerializeField] private Transform _selfPivot;
    [SerializeField] private Transform _cursorPivot;
    private bool _isChangingPosition;

    [SerializeField] private float _moveToCursorTime;
    [SerializeField] private float _moveToStartPlaceTime;

    [SerializeField] private bool _isVisible;

    // TODO: move logic to abstract class
    private void Awake()
    {
        _isPicked = false;
        _isReadyToUse = false;
        _isVisible = false;
    }

    private void OnEnable()
    {
        ScaleColliderHandler.OnEnterCollider += SetReadyToUse;
        ShopHandler.OnShowScaleItems += ChangeVisibility;
    }

    private void OnDisable()
    {
        ScaleColliderHandler.OnEnterCollider -= SetReadyToUse;
        ShopHandler.OnShowScaleItems -= ChangeVisibility;
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
        _animator.SetBool("MouseOver", true);
    }

    private void OnMouseExit()
    {
        _animator.SetBool("MouseOver", false);
    }

    private void ChangeVisibility(bool isActive)
    {
        if (isActive == _isVisible)
        {
            return;
        }

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

    private void OnMouseDown()
    {
        _isChangingPosition = true;
        _currentPivot = _cursorPivot;
        GetComponent<Collider>().enabled = false;
        MoveToPivot(_currentPivot, _moveToCursorTime);
        _isPicked = true;
    }

    private void OnMouseUp()
    {
        _isPicked = false;
        if (_isReadyToUse)
        {
            GetComponent<IScaleItem>().Use();
        }
        //else
        //{
            _isChangingPosition = true;
            Cursor.visible = true;
            _currentPivot = _selfPivot;
            GetComponent<Collider>().enabled = true;
            MoveToPivot(_currentPivot, _moveToStartPlaceTime);
            _isPicked = false;
        //}
    }

    private void SetReadyToUse(bool isReady)
    {
        _isReadyToUse = isReady;
    }

    private void MoveToPivot(Transform target, float timeToMove)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToPivotRoutine(_currentPivot, timeToMove));
    }

    private IEnumerator MoveToPivotRoutine(Transform targetTransform, float time = 0.2f)
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
