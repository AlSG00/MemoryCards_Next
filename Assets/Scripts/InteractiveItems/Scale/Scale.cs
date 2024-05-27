using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Scale : TableItem
{
    private void Awake()
    {
        isVisible = false;
    }

    private void OnEnable()
    {
        ShopHandler.OnShowScale += ChangeVisibility;
        MainMoneyView.UpdatingMainMoneyView += Show;
        StartButton.StartPressed += Hide;
    }

    private void OnDisable()
    {
        ShopHandler.OnShowScale -= ChangeVisibility;
        MainMoneyView.UpdatingMainMoneyView -= Show;
        StartButton.StartPressed -= Hide;
    }
}
