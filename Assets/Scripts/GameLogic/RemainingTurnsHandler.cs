using UnityEngine;

public class RemainingTurnsHandler : MonoBehaviour
{
    [SerializeField] private bool _isActive;
    [SerializeField] private int remainingTurns;

    public static event System.Action<int> OnGUIUpdate;

    private void OnEnable()
    {
        TurnCounter.OnActivateTurnCounter += SetTurnHandlerActive;
        ScrewdriverUseLogic.OnUseScrewdriver += Deactivate;
        NEW_GameProgression.OnTurnsChanged += ChangeRemainingTurns;
        NEW_CardLayoutHandler.OnSetRemainingTurns += SetRemainingTurns;
    }

    private void OnDisable()
    {
        TurnCounter.OnActivateTurnCounter -= SetTurnHandlerActive;
        ScrewdriverUseLogic.OnUseScrewdriver -= Deactivate;
        NEW_GameProgression.OnTurnsChanged -= ChangeRemainingTurns;
        NEW_CardLayoutHandler.OnSetRemainingTurns -= SetRemainingTurns;
    }

    private void Start()
    {
        _isActive = false;
    }

    private void SetTurnHandlerActive(bool isActive)
    {
        _isActive = isActive;
    }

    private void Deactivate()
    {
        Debug.Log($"{gameObject.name} Deactivate()");
        _isActive = false;
    }

    private void ChangeRemainingTurns(bool decreased, int changeValue = 1)
    {
        if (_isActive == false)
        {
            return;
        }

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
            throw new System.Exception("No game loose event"); // TODO: Add lose event
        }

        OnGUIUpdate?.Invoke(remainingTurns);
    }

    private void SetRemainingTurns(int cardsInLayout)
    {
        if (_isActive == false)
        {
            return;
        }

        // TODO: Make complex formula for calculating turns depending on buffs, bebuffs and current round
        remainingTurns = cardsInLayout * 2;
        OnGUIUpdate?.Invoke(remainingTurns);
    }
}
