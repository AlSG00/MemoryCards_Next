using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusButtons : CountableChestStuff
{
    public static event System.Action<int> GiveBonusButtons;

    private protected override void OnMouseDown()
    {
        //_audioSource.PlayOneShot(/*Звук подбираемого нишнятка*/)
        GiveBonusButtons?.Invoke(_quantity);
        gameObject.SetActive(false);
    }
}
