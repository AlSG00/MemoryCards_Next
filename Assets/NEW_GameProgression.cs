using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEW_GameProgression : MonoBehaviour
{

    public NEW_CardGenerator tempCardGenerator;
    public NEW_CardLayoutHandler tempCardLayoutHandler;

    //public int remainingCards;
    public int currentRound = 0;
    public int remainingTurns = 0;
    public int score = 0;

    public static event System.Action OnPressStart;
    public static event System.Action OnGameStartConfirm;

    private void OnEnable()
    {
        StartButton.OnGameStart += StartGame;
        RejectStartButton.OnGameStartReject += RejectGameStart;
        CardComparator.OnPickConfirm += CheckRoundProgression;
    }

    private void OnDisable()
    {
        StartButton.OnGameStart -= StartGame;
        RejectStartButton.OnGameStartReject -= RejectGameStart;
        CardComparator.OnPickConfirm -= CheckRoundProgression;
    }

    private void CheckRoundProgression(List<GameObject> confirmedCards)
    {
        // decrease remaining turns
        if (currentRound == 0 && confirmedCards == null)
        {
            ConfirmGameStart();
        }

        remainingTurns--;
        Debug.Log($"Turns left: {remainingTurns}");
        if (confirmedCards == null)
        {
            return;
        }


        score += 10; //TODO: TEMP. Move to score script


        tempCardLayoutHandler.RemoveConfirmedCards(confirmedCards);

        if (remainingTurns == 0)
        {
            Debug.Log($"<color=orange>NO TURNS LEFT! RESTART GAME</color>");
        }

        if (tempCardGenerator.CheckRemainingCards() == false)
        {
            NextRound();
        }
    }

    private void NextRound()
    {
        remainingTurns = 10;
        currentRound++;
        Debug.Log($"Round: {currentRound}");
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
        tempCardLayoutHandler.PrepareStartLayout();
        OnPressStart?.Invoke();
    }

    private void ConfirmGameStart()
    {
        currentRound++;
        remainingTurns = 10;
        tempCardLayoutHandler.PrepareNewLayout();
        OnGameStartConfirm?.Invoke();
        // reset score
        // reset all debuffs
        // reset all items
        // reset turn counter
        // reset stopwatch
        // reset all cards if has any
        // reset rounds
        // reset card layout
    }

    private void RejectGameStart()
    {
        tempCardLayoutHandler.TakeCardsBack();
    }

    private void FinishGame()
    {

    }
}
