

using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class BackToMenuButton : MenuButton
{
    [SerializeField] private TextMeshProUGUI _warningText;

    public static event System.Action ReturningToMainMenu;

    private void Start()
    {
        _warningText.enabled = false;
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        base.OnPointerEnter(eventData);
        _warningText.enabled = true;
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        _warningText.enabled = false;
    }

    protected override void OnClickAction()
    {
        ReturningToMainMenu?.Invoke();
    }
}
