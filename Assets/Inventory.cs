using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _itemSlots;
    [SerializeField] private Transform[] _cursorPivots;

    public static event System.Action<Transform, Transform> OnReceiveItem;

    private void OnEnable()
    {
        InventoryItem.OnAddToInventory += AddItem;
    }

    private void OnDisable()
    {
        InventoryItem.OnAddToInventory -= AddItem;
    }

    public void AddItem(Transform itemPivot, string itemName)
    {
        foreach (var slot in _itemSlots)
        {
            if (slot.IsAvailable())
            {
                Transform inventorySlotPivot = slot.itemSlotPivots.First(pivot => pivot.name == itemName);
                slot.item = itemPivot;
                Transform itemCursorPivot = _cursorPivots.First(pivot => pivot.name == itemName);
                OnReceiveItem?.Invoke(inventorySlotPivot, itemCursorPivot);
                return;
            }
        }
        OnReceiveItem?.Invoke(null, null);
        throw new System.Exception("Unhandled full inventory exception");
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
