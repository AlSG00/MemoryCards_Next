using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCounterModeSwitcher : MonoBehaviour, IButtonAction
{
    public bool showClock;
    public static event System.Action<bool> OnSwitchMode;

    private void Start()
    {
        OnClickAction();
    }

    public void OnClickAction()
    {
        OnSwitchMode?.Invoke(showClock);
        showClock = !showClock;
    }
}
