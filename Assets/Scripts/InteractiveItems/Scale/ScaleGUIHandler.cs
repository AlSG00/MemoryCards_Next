using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScaleGUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scaleText;

    private void OnEnable()
    {
        InventoryItem.OnReadyToSell += UpdateText;
    }

    private void OnDisable()
    {
        InventoryItem.OnReadyToSell -= UpdateText;
    }

    private void Start()
    {
        UpdateText(0);
    }

    private void UpdateText(int value)
    {
        string result = value.ToString();
        result += ".0";
        _scaleText.text = result;
    }
}
