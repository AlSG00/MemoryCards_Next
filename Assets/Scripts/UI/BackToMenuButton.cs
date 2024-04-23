using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenuButton : MenuButton
{
    public static event System.Action ReturningToMainMenu;

    protected override void OnClickAction()
    {
        ReturningToMainMenu?.Invoke();
    }
}
