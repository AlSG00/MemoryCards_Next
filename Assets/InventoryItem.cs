using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] private string itemName;
    [SerializeField] private Transform _currentPivot;
    [SerializeField] private Transform _previousPivot;
    [SerializeField] private Transform _shopPivot;
    [SerializeField] private Transform _inventoryPivot;
    [SerializeField] private Transform _cursorPivot;

    [SerializeField] private Animator _animator;
    [SerializeField] private bool _isReadyToUse; // Means that item is near 
    [SerializeField] private bool _isPicked;
    [SerializeField] private bool _isBought;
    [SerializeField] private bool _isChangingPosition;
    
    public static event System.Action<Transform, string> OnAddToInventory;

    [SerializeField] private float _moveToInventoryTime;
    [SerializeField] private float _moveToCursorTime;

    [SerializeField] private GameObject _priceTag;
    [SerializeField] private int _buyPrice;
    [SerializeField] private int _sellPrice;
    [SerializeField] private bool _isReadyToSell;

    private void OnEnable()
    {
        Inventory.OnReceiveItem += InitializeForInventory;
    }

    private void OnDisable()
    {
        Inventory.OnReceiveItem -= InitializeForInventory;
    }

    private void Awake()
    {
        _isReadyToUse = false;
        _isPicked = false;
        _isBought = false;
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

    //private void OnMouseEnter()
    //{
    //    Debug.Log("Entered");
    //}

    //private void OnMouseExit()
    //{
    //    Debug.Log("Leaved");
    //}

    private void Buy()
    {
        // TODO: Remove price tag, move to inventory, and write down in inventory slot
    }

    private void Sell()
    {
        // TODO: Move item and destroy
    }

    private void EnableReadyToSell()
    {
        _isReadyToSell = true;
        Debug.Log($"_isReadyToSell: {_isReadyToSell}");
    }

    private void DisableReadyToSell()
    {
        _isReadyToSell = false;
        Debug.Log($"_isReadyToSell: {_isReadyToSell}");
    }

    public void AddToInventory()
    {
        OnAddToInventory?.Invoke(gameObject.transform, itemName);
    }

    private void MoveToPivot(Transform target, float timeToMove)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToPivotRoutine(_currentPivot, timeToMove));
    }

    //private void MoveToInventory(Transform target, Transform )
    //{
    //    if (target == null)
    //    {
    //        return;
    //    }

    //    _inventoryPivot = target;
    //    StopAllCoroutines();
    //    StartCoroutine(MoveToPivotRoutine(_inventoryPivot, _moveToInventoryTime));
    //}

    private void InitializeForInventory(Transform inventoryPivot, Transform cursorPivot)
    {
        if (inventoryPivot == null)
        {
            return;
        }

        if (cursorPivot == null)
        {
            throw new Exception("Cursor pivot not found");
        }

        _inventoryPivot = inventoryPivot;
        _cursorPivot = cursorPivot;
        StopAllCoroutines();
        StartCoroutine(MoveToPivotRoutine(_inventoryPivot, _moveToInventoryTime));
    }

    private void OnMouseDown()
    {
        _isChangingPosition = true;
        //Cursor.visible = false;
        _currentPivot = _cursorPivot;
        GetComponent<BoxCollider>().enabled = false;
        MoveToPivot(_currentPivot, _moveToCursorTime);
    }

    private async void OnMouseUp()
    {
        _isChangingPosition = true;
        Cursor.visible = true;
        _currentPivot = _inventoryPivot;
        GetComponent<BoxCollider>().enabled = true;
        MoveToPivot(_currentPivot, _moveToInventoryTime);
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
