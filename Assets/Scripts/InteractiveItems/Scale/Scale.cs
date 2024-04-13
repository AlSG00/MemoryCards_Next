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
        MainMoneyView.OnMainMoneyViewUpdate += Show;
    }

    private void OnDisable()
    {
        ShopHandler.OnShowScale -= ChangeVisibility;
        MainMoneyView.OnMainMoneyViewUpdate -= Show;
    }
}
