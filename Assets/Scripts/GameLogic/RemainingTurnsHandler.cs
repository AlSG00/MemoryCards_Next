using UnityEngine;

public class RemainingTurnsHandler : MonoBehaviour
{
    [SerializeField] private bool _isActive;
    [SerializeField] private int remainingTurns;

    public static event System.Action<int> OnGUIUpdate;
    public static event System.Action OnOutOfTurns;

    private void OnEnable()
    {
        TurnCounter.OnActivateTurnCounter += SetTurnHandlerActive;
        NEW_GameProgression.OnTurnsChanged += ChangeRemainingTurns;
        NEW_CardLayoutHandler.OnSetRemainingTurns += SetRemainingTurns;
    }

    private void OnDisable()
    {
        TurnCounter.OnActivateTurnCounter -= SetTurnHandlerActive;
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
            remainingTurns = (int)Mathf.Clamp((float)remainingTurns - 1, 0, 999);
        }
        else
        {
            remainingTurns += changeValue;
        }
        OnGUIUpdate?.Invoke(remainingTurns);

        if (remainingTurns == 0)
        {
            OnOutOfTurns?.Invoke();
        }
    }

    private void SetRemainingTurns(int cardsInLayout)
    {
        if (_isActive == false)
        {
            return;
        }

        // TODO: Make complex formula for calculating turns depending on buffs, bebuffs and current round
        remainingTurns = cardsInLayout;
        OnGUIUpdate?.Invoke(remainingTurns);
    }
}
