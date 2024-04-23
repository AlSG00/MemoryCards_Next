using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuHandler : MenuHandler
{
    private void OnEnable()
    {
        NEW_GameProgression.PauseGame += base.SetMenuVisibility;
    }

    private void OnDisable()
    {
        NEW_GameProgression.PauseGame -= base.SetMenuVisibility;
    }
}
