using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PriceTag : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _tagText;

    private void OnEnable()
    {
        InventoryItem.OnInitializeForShop += SetPriceValueText;
    }

    private void OnDisable()
    {
        InventoryItem.OnInitializeForShop -= SetPriceValueText;
    }

    private void SetPriceValueText(int value)
    {
        _tagText.text = value.ToString();
    }
}
