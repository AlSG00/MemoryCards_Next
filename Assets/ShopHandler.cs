using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] private PlayerMoney _money;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private int _shopLevel;
    [SerializeField] private ShopSlots[] _shopSlots;

    [SerializeField] private Transform[] _sellPivots; // Pivot to move sold item off-screen



    public static event System.Action<Transform, string, int> OnEnoughMoney;

    private void OnEnable()
    {
        InventoryItem.OnBuyItem += BuyItem;
    }

    private void OnDisable()
    {
        InventoryItem.OnBuyItem -= BuyItem;
    }

    private void BuyItem(InventoryItem item, Transform itemPivot, string itemName, int itemPrice)
    {
        if (_money.IsEnoughCurrentGameMoney(itemPrice) == false)
        {
            return;
        }

        if (_inventory.AddBoughtItem(item, itemPivot, itemName) == false)
        {
            return;
        }

        _money.GetCurrentGameMoney(itemPrice);
    }

    private void SellItem()
    {
        // Remove from inventory
        // Add money
        //
    }

    private void GenerateGoods()
    {
        
    }

    private void ShowStore()
    {
        ShopSlots shopSlots = _shopSlots[_shopLevel];
        foreach (var slot in shopSlots.slots)
        {
            // TODO: Generate and assign to pivot;
        }
    }

    private void HideStore()
    {

    }

    public class ShopSlots
    {
        public Transform[] slots; 
    }
}
