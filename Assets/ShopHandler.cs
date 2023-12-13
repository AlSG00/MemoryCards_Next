using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    [SerializeField] private PlayerMoney _money;
    [SerializeField] private Inventory _inventory;

    public static event System.Action<Transform, string, int> OnEnoughMoney;

    private void OnEnable()
    {
        InventoryItem.OnBuyItem += Buyitem;
    }

    private void OnDisable()
    {
        InventoryItem.OnBuyItem -= Buyitem;
    }

    private void Buyitem(Transform itemPivot, string itemName, int itemPrice)
    {
        if (_money.IsEnoughCurrentGameMoney(itemPrice) == false)
        {
            return;
        }

        if (_inventory.AddBoughtItem(itemPivot, itemName) == false)
        {
            return;
        }

        _money.GetCurrentGameMoney(itemPrice);
    }
}
