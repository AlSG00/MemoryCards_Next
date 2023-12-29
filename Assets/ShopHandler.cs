using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] private PlayerMoney _money;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private int _shopLevel;
    [SerializeField] private ShopSlots[] _shopSlots;

    [SerializeField] private Transform _generatedItemPivot;
    [SerializeField] private Transform[] _sellPivots; // Pivot to move sold item off-screen
    [SerializeField] private InventoryItem[] _earlyGameGoods;
    [SerializeField] private InventoryItem[] goods;



    public static event System.Action<Transform, Transform, string, int> OnEnoughMoney;
    public static event System.Action<bool> OnShowScale;
    public static event System.Action<bool> OnShowScaleItems;
    public static event System.Action<InventoryItem> OnBoughtItemAdd;
    public static event System.Action<InventoryItem, Transform> OnItemGenerated;
    public static event System.Action<InventoryItem, Transform> OnItemRemoved;

    private void OnEnable()
    {
        InventoryItem.OnBuyItem += BuyItem;
        NEW_GameProgression.OnStartBuyRound += EnableStore;
    }

    private void OnDisable()
    {
        InventoryItem.OnBuyItem -= BuyItem;
        NEW_GameProgression.OnStartBuyRound -= EnableStore;
    }

    private void BuyItem(InventoryItem item, Transform itemPivot, string itemName, int itemPrice)
    {
        if (_money.IsEnoughCurrentGameMoney(itemPrice) == false)
        {
            Debug.Log("No money");
            return;
        }

        if (_inventory.AddBoughtItem(item, itemPivot, itemName) == false)
        {
            Debug.Log("No free space");
            return;
        }

        _money.GetCurrentGameMoney(itemPrice);
        //OnEnoughMoney?.Invoke(item, item.transform, itemName, itemPrice);
        OnBoughtItemAdd?.Invoke(item);
        ShopSlots shopSlots = _shopSlots[_shopLevel];
        _shopSlots[_shopLevel].items[System.Array.IndexOf(_shopSlots[_shopLevel].items, item)] = null;
    }

    private void SellItem()
    {
        // Remove from inventory
        // Add money
        //
    }

    private void GenerateGoods()
    {
        ShopSlots shopSlots = _shopSlots[_shopLevel];

        for (int i = 0; i < shopSlots.slots.Length; i++)
        {
            var itemToGenerate = _earlyGameGoods[Random.Range(0, _earlyGameGoods.Length)];
            var item = Instantiate(itemToGenerate, _generatedItemPivot.position, _generatedItemPivot.rotation);
            shopSlots.items[i] = item;
            OnItemGenerated?.Invoke(item, shopSlots.slots[i]);
        }

        //foreach (var slot in shopSlots.slots)
        //{
        //    // TODO: Generate and assign to pivots;
        //    var itemToGenerate = _earlyGameGoods[Random.Range(0, _earlyGameGoods.Length)];
        //    var item =  Instantiate(itemToGenerate, _generatedItemPivot.position, _generatedItemPivot.rotation);
        //    shopSlots.items = item;
        //    OnItemGenerated?.Invoke(item, slot);
        //}
        
        Debug.Log("Generated");
    }

    private void RemoveGoods()
    {
        ShopSlots shopSlots = _shopSlots[_shopLevel];

        for (int i = 0; i < shopSlots.slots.Length; i++)
        {
            //var itemToGenerate = _earlyGameGoods[Random.Range(0, _earlyGameGoods.Length)];
            //var item = Instantiate(itemToGenerate, _generatedItemPivot.position, _generatedItemPivot.rotation);
            if (shopSlots.items[i] != null)
            {
                OnItemRemoved?.Invoke(shopSlots.items[i], _generatedItemPivot);
                shopSlots.items[i] = null;
            }
        }
    }

    private void EnableStore(bool isEnabled)
    {
        if (isEnabled)
        {
            ShowStore();
        }
        else
        {
            HideStore();
        }
    }

    private void ShowStore()
    {
        GenerateGoods();
        OnShowScale?.Invoke(true);
        OnShowScaleItems?.Invoke(true);
    }

    private void HideStore()
    {
        RemoveGoods();
        OnShowScale?.Invoke(false);
        OnShowScaleItems?.Invoke(false);
    }

    [System.Serializable]
    public class ShopSlots
    {
        public Transform[] slots;
        public InventoryItem[] items;
    }
}
