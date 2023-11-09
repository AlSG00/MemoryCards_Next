using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private SessionProgressHandler session;
    [SerializeField] private PauseMenuHandler pauseMenuHandler;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandlePauseMenu();
        }
    }

    private void HandlePauseMenu()
    {
        if (!session.isGamePaused)
        {
            pauseMenuHandler.EnterPauseMenu();
        }
        else
        {
            pauseMenuHandler.ExitPauseMenu();
        }
    }
}
