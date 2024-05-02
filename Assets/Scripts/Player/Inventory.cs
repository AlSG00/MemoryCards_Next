using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _itemSlots;
    [SerializeField] private Transform[] _cursorPivots;
    [SerializeField] private Transform _removeOnEndGamePivot;

    public static event System.Action<InventoryItem, Transform, Transform> OnReceiveItem;
    public static event System.Action<InventoryItem> OnBoughtItemAdd;

    private void OnEnable()
    {
        InventoryItem.OnAddToInventory += AddItem;
        InventoryItem.OnRemoveFromInventory += RemoveItem;
        //NEW_GameProgression.ga += DestroyAllItems;
        RejectStartButton.OnGameStartReject += DestroyAllItems;
        ScaleSuspend.OnSuspendGame += SaveAndClear;
        StartButton.StartPressed += DestroyAllItems;
        BackToMenuButton.ReturningToMainMenu += DestroyAllItems;
    }

    private void OnDisable()
    {
        InventoryItem.OnAddToInventory -= AddItem;
        InventoryItem.OnRemoveFromInventory -= RemoveItem;
        //NEW_GameProgression.OnGameStartConfirm -= DestroyAllItems;
        RejectStartButton.OnGameStartReject -= DestroyAllItems;
        ScaleSuspend.OnSuspendGame -= SaveAndClear;
        StartButton.StartPressed -= DestroyAllItems;
        BackToMenuButton.ReturningToMainMenu -= DestroyAllItems;
    }


    // TODO: Rework. Convoluted
    public void AddItem(InventoryItem item, Transform itemPivot, string itemName)
    {
        if (FoundAvailableSlot(item, itemPivot, itemName))
        {
            return;
        }

        OnReceiveItem?.Invoke(item, null, null);
        throw new System.Exception("Unhandled full inventory exception");
    }

    public void RemoveItem(InventoryItem item, Transform inventoryPivot)
    {
        foreach (var slot in _itemSlots)
        {
            if (slot.itemSlotPivots.Contains(inventoryPivot))
            {
                slot.item = null;
            }
        }
    }

    public void DestroyAllItems()
    {
        foreach (var slot in _itemSlots)
        {
            if (slot.item != null)
            {
                slot.item.gameObject.GetComponent<InventoryItem>().MoveToPivot(_removeOnEndGamePivot, 0.2f); // TODO: move to var
                //Destroy(slot.item.gameObject);
                slot.item = null;
            }
        }

        DestroyAllItemsImmediately();
    }

    public void DestroyAllItemsImmediately()
    {
        foreach (var slot in _itemSlots)
        {
            if (slot.item != null)
            {
                //slot.item.gameObject.
                Destroy(slot.item.gameObject);
                slot.item = null;
            }
        }
    }

    //public void DestroyAllItems()
    //{
    //    foreach (var slot in _itemSlots)
    //    {
    //        if (slot.item != null)
    //        {
    //            //slot.item.gameObject.GetComponent<InventoryItem>().RemoveOnGameEnd();
    //            Destroy(slot.item.gameObject);
    //            slot.item = null;
    //        }
    //    }
    //}

    public int GetItemsInInventoryCount()
    {
        int itemsCount = 0;
        foreach (var slot in _itemSlots)
        {
            if (slot.isUnlocked && slot.item != null)
            {
                itemsCount++;
            }
        }

        return itemsCount;
    }

    public bool AddBoughtItem(InventoryItem item, Transform itemPivot, string itemName/*, int itemPrice*/)
    {
        if (FoundAvailableSlot(item, itemPivot, itemName))
        {
            OnBoughtItemAdd?.Invoke(item);
            return true;
        }

        OnReceiveItem?.Invoke(item, null, null);
        // Add event to indicate that you cant buy an item

        return false;
    }

    // TODO: Rework. Too convoluted
    private bool FoundAvailableSlot(InventoryItem item, Transform itemPivot, string itemName)
    {
        foreach (var slot in _itemSlots)
        {
            if (slot.IsUnlocked() && slot.item == null)
            {
                Transform inventorySlotPivot = slot.itemSlotPivots.First(pivot => pivot.name == itemName);
                slot.item = itemPivot;
                Transform itemCursorPivot = _cursorPivots.First(pivot => pivot.name == itemName);
                OnReceiveItem?.Invoke(item, inventorySlotPivot, itemCursorPivot);
                return true;
            }
        }
        return false;
    }

    public bool HasFreeSlotDebug() // TODO: Check if still needed
    {
        foreach (var slot in _itemSlots)
        {
            if (slot.IsUnlocked() && slot.item == null)
            {
                return true;
            }
        }
        return false;
    }

    private void SaveAndClear()
    {
        // TODO: Save items
        DestroyAllItems();
    }

    [System.Serializable]
    public sealed class InventorySlot
    {
        public bool isUnlocked;
        public Transform[] itemSlotPivots;
        public Transform item;

        public bool IsUnlocked()
        {
            if (isUnlocked == false)
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
