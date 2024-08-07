using System;
using UnityEngine;
using System.Linq;

public class InventoryItem : InteractiveItem
{
    public int _buyPrice;
    [SerializeField] private string itemName;
    [SerializeField] private ItemType _type;
    private Transform _inventoryPivot;

    [Header("Shop info")]
    [SerializeField] private bool _mustBuy;
    [SerializeField] private GameObject _priceTag;
    [SerializeField] private int _sellPrice;
    private bool _isReadyToSell;
    [SerializeField] private Vector3 _shopPivotOffset;

    public static event System.Action<InventoryItem, Transform, string> OnAddToInventory;
    public static event System.Action<InventoryItem, Transform> OnRemoveFromInventory;
    public static event System.Action<int> OnReadyToSell;
    public static event System.Action<InventoryItem, Transform, int> OnSellingItem;
    public static event System.Action<InventoryItem, Transform, string, int> OnBuyItem;
    public static event System.Action OnInitializeForShop;
    public static event System.Action<bool> OnPick;

    public ItemType ItemType
    {
        get
        {
            return _type;
        }

        private set
        {
            _type = value;
        }
    }
    

    private void OnEnable()
    {
        Inventory.OnReceiveItem += InitializeForInventory;
        ScaleColliderHandler.OnEnterCollider += SetReadyToSell;
        Inventory.OnBoughtItemAdd += Buy;
        ShopHandler.OnItemRemove += RemoveAsShopGood;
        ItemApplyingTriggerHandler.OnEnterTrigger += SetReadyToUse;
    }

    private void OnDisable()
    {
        Inventory.OnReceiveItem -= InitializeForInventory;
        ScaleColliderHandler.OnEnterCollider -= SetReadyToSell;
        Inventory.OnBoughtItemAdd -= Buy;
        ShopHandler.OnItemRemove -= RemoveAsShopGood;
        ItemApplyingTriggerHandler.OnEnterTrigger -= SetReadyToUse;
    }

    private void Awake()
    {
        _isReadyToUse = false;
        _isPicked = false;
        _mustBuy = false;
        _isChangingPosition = false;
    }

    private void Buy(InventoryItem item)
    {
        if (item != this)
        {
            return;
        }

        _mustBuy = false;
        _priceTag.SetActive(false);
    }

    private void SetReadyToSell(bool isReady)
    {
        if (_isPicked == false)
        {
            return;
        }

        _isReadyToSell = isReady;
        if (isReady)
        {
            OnReadyToSell?.Invoke(_sellPrice);
        }
        else
        {
            OnReadyToSell?.Invoke(0);
        }
    }

    private protected void SetReadyToUse(bool isReady, ItemType[] type)
    {
        if (_isPicked == false)
        {
            return;
        }

        if (type.Contains(_type) == false)
        {
            return;
        }

        base.SetReadyToUse(isReady);
    }

    public void AddToInventory()
    {
        OnAddToInventory?.Invoke(this, gameObject.transform, itemName);
    }

    private void RemoveAsShopGood(InventoryItem item, Transform pivot)
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
        MoveToPivot(_currentPivot, 0.4f);
        Destroy(gameObject, 3);
    }

    private void RemoveAsUsed()
    {
        OnRemoveFromInventory?.Invoke(this, _inventoryPivot);
        Destroy(gameObject);
    }

    #region INITIALIZATION

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
        MoveToPivot(_inventoryPivot, _moveToStandartPositionTime);
    }

    internal void InitializeForShop(Transform shopPivot)
    {
        if (shopPivot == null)
        {
            throw new System.Exception("Shop pivot not found");
        }

        _isChangingPosition = true;
        _mustBuy = true;
        OnInitializeForShop?.Invoke();
        MoveToPositionWithOffset(shopPivot, _shopPivotOffset, 5f);
    }
    #endregion

    private protected override void OnMouseDown()
    {
        if (_mustBuy)
        {
            return;
        }

        _isChangingPosition = true;
        _currentPivot = _cursorPivot;
        gameObject.GetComponent<Collider>().enabled = false;
        MoveToPivot(_currentPivot, _moveToCursorTime);
        _isPicked = true;
        OnPick?.Invoke(_isPicked);
    }

    private protected override void OnMouseUp()
    {
        _isPicked = false;
        OnPick?.Invoke(_isPicked);
        if (_mustBuy)
        {
            OnBuyItem?.Invoke(this, gameObject.transform, itemName, _buyPrice);
            return;
        }

        if (_isReadyToSell)
        {
            OnSellingItem?.Invoke(this, _inventoryPivot, _sellPrice);
            OnReadyToSell?.Invoke(0);
            return;
        }

        if (_isReadyToUse)
        {
            gameObject.GetComponent<IUsable>().Use();
            RemoveAsUsed();
            return;
        }

        _isChangingPosition = true;
        Cursor.visible = true;
        _currentPivot = _inventoryPivot;
        gameObject.GetComponent<Collider>().enabled = true;
        MoveToPivot(_currentPivot, _moveToStandartPositionTime);
    }
}
