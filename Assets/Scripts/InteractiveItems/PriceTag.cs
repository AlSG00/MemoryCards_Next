using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(InventoryItem))]
public class PriceTag : MonoBehaviour
{
    [SerializeField] private InventoryItem _item;
    [SerializeField] private TextMeshProUGUI _tagText;

    private void OnEnable()
    {
        InventoryItem.OnInitializeForShop += SetPriceValueText;
    }

    private void OnDisable()
    {
        InventoryItem.OnInitializeForShop -= SetPriceValueText;
    }

    private void SetPriceValueText()
    {
        _tagText.text = _item._buyPrice.ToString();
    }
}