using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MenuButton))]
public class StartButton : MonoBehaviour, IButtonAction
{
    public static event System.Action OnGameStart;
    public void OnClickAction()
    {
        OnGameStart?.Invoke();
    }
}
