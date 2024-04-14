using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
