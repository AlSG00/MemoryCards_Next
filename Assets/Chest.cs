using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : TableItem
{
    public static event System.Action<ItemType> OnOpenChest;

    private void OnEnable()
    {
        GameProgression.ActivateChest += ChangeVisibility;
        KeyUseLogic.OnUseKey += OpenWithKey;
    }

    private void OnDisable()
    {
        GameProgression.ActivateChest -= ChangeVisibility;
        KeyUseLogic.OnUseKey += OpenWithKey;
    }

    //private protected override void Show()
    //{
    //    if (isVisible == false)
    //    {
    //        isVisible = true;
    //        _animator.SetTrigger("Show");
    //    }
    //}

    //private protected override void Hide()
    //{
    //    if (isVisible)
    //    {
    //        isVisible = false;
    //        _animator.SetTrigger("Hide");
    //    }
    //}

    private void OpenWithKey(ItemType keyType)
    {
        if (isVisible == false)
        {
            return;
        }

        //isVisible = false;
        _animator.SetTrigger("Deactivate");
        OnOpenChest?.Invoke(keyType);
    }
}
