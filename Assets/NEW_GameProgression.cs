using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEW_GameProgression : MonoBehaviour
{

    public NEW_CardGenerator tempCardGenerator;
    public NEW_CardLayoutHandler tempCardLayoutHandler;

    public int remainingCards;
    public int remainingturns;
    public int score = 0;


    private void OnEnable()
    {
        StartButton.OnGameStart += StartGame;
        CardComparator.OnMatchConfirm += CheckRoundProgression;
    }

    private void OnDisable()
    {
        StartButton.OnGameStart -= StartGame;
        CardComparator.OnMatchConfirm -= CheckRoundProgression;
    }

    private void CheckRoundProgression(List<GameObject> confirmedCards)
    {
        // decrease remaining turns
        score += 10; //TODO: TEMP. Move to score script
        tempCardLayoutHandler.RemoveConfirmedCards(confirmedCards);

        if (tempCardGenerator.CheckRemainingCards() == false)
        {
            NextRound();
        }
    }

    private void NextRound()
    {
        tempCardLayoutHandler.PrepareNewLayout();
        // Decide which round is next (store or cards)
        // Set new layout
        // Generate cards
        // Reset turns
        // Maybe smth else
        // Next
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
