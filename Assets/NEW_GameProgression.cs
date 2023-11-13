using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEW_GameProgression : MonoBehaviour
{

    public NEW_CardGenerator tempCardGenerator;
    public NEW_CardLayoutHandler tempCardLayoutHandler;

    private void OnEnable()
    {
        StartButton.OnGameStart += StartGame;
    }

    private void OnDisable()
    {
        StartButton.OnGameStart -= StartGame;
    }

    private void StartGame()
    {
        tempCardLayoutHandler.PrepareNewLayout();
        // reset score
        // reset all debuffs
        // reset all items
        // reset turn counter
        // reset stopwatch
        // reset all cards if has any
        // reset rounds
        // reset card layout
    }

    private void FinishGame()
    {

    }
}
