using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemainingTurnsHandler : MonoBehaviour
{
    [SerializeField] private int remainingTurns;

    public static event System.Action<int> OnGUIUpdate;

    private void OnEnable()
    {
        NEW_GameProgression.OnTurnsChanged += ChangeRemainingTurns;
        NEW_CardLayoutHandler.OnSetRemainingTurns += SetRemainingTurns;
    }

    private void OnDisable()
    {
        NEW_GameProgression.OnTurnsChanged -= ChangeRemainingTurns;
        NEW_CardLayoutHandler.OnSetRemainingTurns -= SetRemainingTurns;
    }

    private void ChangeRemainingTurns(bool decreased, int changeValue = 1)
    {
        if (decreased)
        {
            remainingTurns -= changeValue;
        }
        else
        {
            remainingTurns += changeValue;
        }

        if (remainingTurns <= 0)
        {
            remainingTurns = 0; // So without negatives on turn counter
            Debug.Log("<color=orange>No turns left</color>");
            // TODO: Start lose event
        }

        OnGUIUpdate?.Invoke(remainingTurns);
    }

    private void SetRemainingTurns(int cardsInLayout)
    {
        // TODO: Make complex formula for calculating turns depending on buffs, bebuffs and current round
        remainingTurns = cardsInLayout * 2;
        OnGUIUpdate?.Invoke(remainingTurns);
    }
}
