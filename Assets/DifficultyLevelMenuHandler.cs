using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevelMenuHandler : MenuHandler
{
    private void OnEnable()
    {
        StartButton.OnGameStart += ShowMenu;
        RejectStartButton.OnGameStartReject += HideMenu;
    }

    private void OnDisable()
    {
        StartButton.OnGameStart -= ShowMenu;
        RejectStartButton.OnGameStartReject -= HideMenu;
    }
}
