using UnityEngine;

public class RemainingTurnsHandler : MonoBehaviour
{
    [SerializeField] private bool _isActive;
    [SerializeField] private int remainingTurns;

    public static event System.Action<int> OnGUIUpdate;
    public static event System.Action OutOfTurns;

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
            OutOfTurns?.Invoke();
        }
    }

    private void SetRemainingTurns(int cardsInLayout, int currentRound)
    {
        if (_isActive == false)
        {
            return;
        }

        // TODO: Finish
        if (currentRound < 30)
        {
            remainingTurns = cardsInLayout * 2 - currentRound / 5;
        }
        else 
        {
            remainingTurns = cardsInLayout * 2 - currentRound / 7;
        }


        remainingTurns = cardsInLayout; 
        OnGUIUpdate?.Invoke(remainingTurns);
    }
}
