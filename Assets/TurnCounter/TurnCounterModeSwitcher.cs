using UnityEngine;

public class TurnCounterModeSwitcher : MonoBehaviour, IButtonAction
{
    private bool _showClock = false;

    public static event System.Action<bool> OnSwitchMode;

    private void Start()
    {
        OnSwitchMode?.Invoke(_showClock);
    }

    public void OnClickAction()
    {
        _showClock = !_showClock;
        OnSwitchMode?.Invoke(_showClock);
    }
}
