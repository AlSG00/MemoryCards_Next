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
    [SerializeField] private bool _isReadyToUse; // Means that item is near the object it could be assgned
    [SerializeField] private bool _isPicked;
    [SerializeField] private bool _mustBuy;
    private bool _isChangingPosition;
    
    public static event System.Action<InventoryItem, Transform, string> OnAddToInventory;
    public static event System.Action<int> OnReadyToSell;
    public static event System.Action<InventoryItem, Transform, string, int> OnBuyItem;
    public static event System.Action OnInitializeForShop;

    [SerializeField] private float _moveToInventoryTime;
    [SerializeField] private float _moveToCursorTime;

    [SerializeField] private GameObject _priceTag;
    public int _buyPrice;
    [SerializeField] private int _sellPrice;
    [SerializeField] private bool _isReadyToSell;

    private void OnEnable()
    {
        Inventory.OnReceiveItem += InitializeForInventory;
        ScaleColliderHandler.OnEnterCollider += EnableReadyToSell;
        Inventory.OnBoughtItemAdd += Buy;
        ShopHandler.OnItemGenerated += InitializeForShop;
        ShopHandler.OnItemRemoved += RemoveItem;
    }

    private void OnDisable()
    {
        Inventory.OnReceiveItem -= InitializeForInventory;
        ScaleColliderHandler.OnEnterCollider -= EnableReadyToSell;
        Inventory.OnBoughtItemAdd -= Buy;
        ShopHandler.OnItemGenerated -= InitializeForShop;
        ShopHandler.OnItemRemoved -= RemoveItem;
    }

    private void Awake()
    {
        _isReadyToUse = false;
        _isPicked = false;
        _mustBuy = false;
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

    private void Buy(InventoryItem item)
    {
        // TODO: Remove price tag, move to inventory, and write down in inventory slot
        if (item != this)
        {
            return;
        }

        _mustBuy = false;
        _priceTag.SetActive(false);
    }

    private void Sell()
    {
        // TODO: Move item and destroy
    }

    private void EnableReadyToSell(bool enable)
    {
        if (_isPicked == false)
        {
            return;
        }

        if (enable)
        {
            _isReadyToSell = true;
            OnReadyToSell?.Invoke(_sellPrice);
            Debug.Log($"_isReadyToSell: {_isReadyToSell} : {_sellPrice}");
        }
        else
        {
            _isReadyToSell = false;
            OnReadyToSell?.Invoke(0);
            Debug.Log($"_isReadyToSell: {_isReadyToSell}");
        }
    }

    //private void DisableReadyToSell()
    //{
        
    //}

    public void AddToInventory()
    {
        OnAddToInventory?.Invoke(this, gameObject.transform, itemName);
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

    private void InitializeForInventory(InventoryItem item, Transform inventoryPivot, Transform cursorPivot)
    {
        if (item != this)
        {
            return;
        }

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
        _currentPivot = _inventoryPivot;
        StopAllCoroutines();
        StartCoroutine(MoveToPivotRoutine(_inventoryPivot, _moveToInventoryTime));
    }

    private void InitializeForShop(InventoryItem item, Transform shopPivot)
    {
        if (item != this)
        {
            return;
        }

        if (shopPivot == null)
        {
            throw new System.Exception("Shop pivot not found");
        }

        _isChangingPosition = true;
        _mustBuy = true;
        _currentPivot = shopPivot;
        OnInitializeForShop?.Invoke();
        MoveToPivot(_currentPivot, 1);
    }

    private void RemoveItem(InventoryItem item, Transform pivot)
    {
        if (item != this)
        {
            return;
        }

        if (pivot == null)
        {
            throw new System.Exception("Shop pivot not found");
        }

        _isChangingPosition = true;
        _currentPivot = pivot;
        MoveToPivot(_currentPivot, 1);
        Destroy(gameObject, 3f);
    }

    private void OnMouseDown()
    {
        if (_mustBuy)
        {
            return;
        }

        _isChangingPosition = true;
        //Cursor.visible = false;
        _currentPivot = _cursorPivot;
        GetComponent<BoxCollider>().enabled = false;
        MoveToPivot(_currentPivot, _moveToCursorTime);
        _isPicked = true;
    }

    private async void OnMouseUp()
    {
        if (_mustBuy)
        {
            OnBuyItem?.Invoke(this, gameObject.transform, itemName, _buyPrice);
        }
        else
        {
            _isChangingPosition = true;
            Cursor.visible = true;
            _currentPivot = _inventoryPivot;
            GetComponent<BoxCollider>().enabled = true;
            MoveToPivot(_currentPivot, _moveToInventoryTime);
            _isPicked = false;
        }
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
