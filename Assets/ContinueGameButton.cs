using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueGameButton : MenuButton
{
    public static event System.Action ContinueButtonClicked;
    protected override void OnClickAction()
    {
        ContinueButtonClicked?.Invoke();
    }
}
