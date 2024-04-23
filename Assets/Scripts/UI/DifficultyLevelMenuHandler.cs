using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyLevelMenuHandler : MenuHandler
{
    private void OnEnable()
    {
        LeaveDifficultyMenuButton.LeavedDifficultyMenu += HideMenu;
        SetDifficultyButton.DifficultyPicked += HideMenu;
        StartButton.StartPressed += ShowMenu;
        //RejectStartButton.OnGameStartReject += HideMenu;
    }

    private void OnDisable()
    {
        LeaveDifficultyMenuButton.LeavedDifficultyMenu -= HideMenu;
        SetDifficultyButton.DifficultyPicked -= HideMenu;
        StartButton.StartPressed -= ShowMenu;
        //RejectStartButton.OnGameStartReject -= HideMenu;
    }
}
