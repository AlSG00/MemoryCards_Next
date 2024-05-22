using UnityEngine;
using TMPro;

public class ScaleGUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scaleText;

    private void OnEnable()
    {
        NEW_GameProgression.OnStartBuyRound += ResetText;
        InventoryItem.OnReadyToSell += UpdateText;
        MainMoneyView.UpdatingMainMoneyCounter += UpdateText;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnStartBuyRound -= ResetText;
        InventoryItem.OnReadyToSell -= UpdateText;
        MainMoneyView.UpdatingMainMoneyCounter -= UpdateText;
    }

    private void Start()
    {
        ResetText();
    }

    private void ResetText(bool isBuyRoundStarted = true)
    {
        UpdateText(0);
    }

    private void UpdateText(int value)
    {
        string result = value.ToString("f1", System.Globalization.CultureInfo.InvariantCulture); //So float digit showed up with dot instead of comma
        _scaleText.text = result;
    }
}
