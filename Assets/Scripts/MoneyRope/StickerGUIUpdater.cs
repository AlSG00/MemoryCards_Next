using TMPro;
using UnityEngine;

public class StickerGUIUpdater : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _stickerText;

    private void OnEnable()
    {
        PlayerMoney.OnMoneyAmountChanged += UpdateText;
    }

    private void OnDisable()
    {
        PlayerMoney.OnMoneyAmountChanged -= UpdateText;
    }

    private void Start()
    {
        UpdateText(0);
    }

    private void UpdateText(int amount)
    {
        _stickerText.text = amount.ToString();
    }
}
