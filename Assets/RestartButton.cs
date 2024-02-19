using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartButton : MonoBehaviour, IButtonAction
{
    public static event System.Action OnGameRestart;
    public void OnClickAction()
    {
        OnGameRestart?.Invoke();
    }
}
