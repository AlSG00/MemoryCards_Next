using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugAddHammer : MonoBehaviour
{
    [SerializeField] private InventoryItem _hammerPrefab;
    public void AddHammer()
    {
        var hammer = Instantiate(_hammerPrefab);
        hammer.AddToInventory();
    }
}
