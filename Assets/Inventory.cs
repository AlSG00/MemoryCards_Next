using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private InventorySlot[] _itemSlots;

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    public void AddItem(Transform itemPivot)
    {
        foreach (var slot in _itemSlots)
        {
            if (slot.IsAvailable())
            {

            }
        }
    }

    [System.Serializable]
    private class InventorySlot
    {
        [SerializeField] private bool isAvailable;
        [SerializeField] private Transform itemSlot;
        [SerializeField] private Transform item;

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
