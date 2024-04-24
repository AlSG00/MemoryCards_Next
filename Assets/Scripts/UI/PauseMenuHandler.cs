using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuHandler : MenuHandler
{
    private void OnEnable()
    {
        NEW_GameProgression.PauseGame += base.SetMenuVisibility;
        BackToMenuButton.ReturningToMainMenu += HideMenu;
    }

    private void OnDisable()
    {
        NEW_GameProgression.PauseGame -= base.SetMenuVisibility;
        BackToMenuButton.ReturningToMainMenu -= HideMenu;
    }
}
