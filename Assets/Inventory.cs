using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _itemSlots;
    [SerializeField] private Transform[] _cursorPivots;

    public static event System.Action<Transform, Transform> OnReceiveItem;
    public static event System.Action OnBoughtItemAdded;

    private void OnEnable()
    {
        InventoryItem.OnAddToInventory += AddItem;
        //InventoryItem.OnBuyItem += AddBoughtItem;
        //ShopHandler.OnEnoughMoney += AddBoughtItem;
    }

    private void OnDisable()
    {
        InventoryItem.OnAddToInventory -= AddItem;
        // InventoryItem.OnBuyItem += AddBoughtItem;
        //ShopHandler.OnEnoughMoney -= AddBoughtItem;
    }

    public void AddItem(InventoryItem item, Transform itemPivot, string itemName)
    {
        if (FoundAvailableSlot(itemPivot, itemName))
        {
            return;
        }
        ��������� �����
        ����� ����������� ���� ����������� ��������
        ������� ������� ������ �� �������, � � �������� ������� ������ �� ��� �����


        OnReceiveItem?.Invoke(null, null);
        throw new System.Exception("Unhandled full inventory exception");
    }

    public bool AddBoughtItem(Transform itemPivot, string itemName/*, int itemPrice*/)
    {
        if (FoundAvailableSlot(itemPivot, itemName))
        {
            OnBoughtItemAdded?.Invoke();
            return true;
        }

        

        OnReceiveItem?.Invoke(null, null);
        // Add event to indicate that you cant buy an item

        return false;
        //throw new System.Exception("Unhandled full inventory exception");
    }

    private bool FoundAvailableSlot(Transform itemPivot, string itemName)
    {
        foreach (var slot in _itemSlots)
        {
            if (slot.IsAvailable())
            {
                Transform inventorySlotPivot = slot.itemSlotPivots.First(pivot => pivot.name == itemName);
                slot.item = itemPivot;
                Transform itemCursorPivot = _cursorPivots.First(pivot => pivot.name == itemName);
                OnReceiveItem?.Invoke(inventorySlotPivot, itemCursorPivot);
                return true;
            }
        }
        return false;
    }



    [System.Serializable]
    public class InventorySlot
    {
        public bool isAvailable;
        public Transform[] itemSlotPivots;
        public Transform item;

        public bool IsAvailable()
        {
            if (isAvailable == false)
            {
                return false;
            }

            if (item != null)
            {
                return false;
            }

            return true;
        }
    }
}
