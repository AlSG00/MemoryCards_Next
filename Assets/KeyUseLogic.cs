using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUseLogic : MonoBehaviour, IUsable
{
    public delegate void UseAction(ItemType usedKeyType);

    public static UseAction OnUseKey;

    public void Use()
    {
        ItemType keyType = gameObject.GetComponent<InventoryItem>().ItemType;
        OnUseKey?.Invoke(keyType);
    }
}
