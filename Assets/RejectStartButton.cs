using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MenuButton))]
public class RejectStartButton : MonoBehaviour, IButtonAction
{
    public static event System.Action OnGameStartReject;
    public void OnClickAction()
    {
        OnGameStartReject?.Invoke();
    }
}
