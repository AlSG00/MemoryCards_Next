using TMPro;
using UnityEngine;

public class ScaleGUIHandler : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scaleText;

    private void OnEnable()
    {
        GameProgression.OnStartBuyRound += ResetText;
        InventoryItem.OnReadyToSell += UpdateText;
        MainMoneyView.UpdatingMainMoneyScaleCounter += UpdateText;
    }

    private void OnDisable()
    {
        GameProgression.OnStartBuyRound -= ResetText;
        InventoryItem.OnReadyToSell -= UpdateText;
        MainMoneyView.UpdatingMainMoneyScaleCounter -= UpdateText;
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
