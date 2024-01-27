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
    }

    private void OnDisable()
    {
        ShopHandler.OnShowScale -= ChangeVisibility;
    }
}
