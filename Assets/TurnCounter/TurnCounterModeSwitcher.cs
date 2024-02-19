using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCounterModeSwitcher : MonoBehaviour, IButtonAction
{
    public bool showClock;
    public static event System.Action<bool> OnSwitchMode;

    private void Start()
    {
        OnSwitchMode?.Invoke(showClock);
    }

    public void OnClickAction()
    {
        showClock = !showClock;
        OnSwitchMode?.Invoke(showClock);
    }
}
