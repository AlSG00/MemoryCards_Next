using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusCoins : CountableChestStuff
{
    public static event System.Action<int> GiveBonusCoins;

    private protected override void OnMouseDown()
    {
        //_audioSource.PlayOneShot(/*Звук подбираемого нишнятка*/)
        GiveBonusCoins?.Invoke(_quantity);
        gameObject.SetActive(false);
    }
}
