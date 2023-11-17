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

    [Tooltip("Each round divisible by this digit will be a buy round")]
    public int buyRound;

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
        if (currentRound == 0 && confirmedCards != null)
        {
            tempCardLayoutHandler.RemoveCertainCards(confirmedCards);
            ConfirmGameStart();
            return;
        }

        remainingTurns--;
        Debug.Log($"Turns left: {remainingTurns}");
        if (confirmedCards == null)
        {
            return;
        }


        score += 10; //TODO: TEMP. Move to score script


        tempCardLayoutHandler.RemoveCertainCards(confirmedCards);

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
        currentRound++;
        Debug.Log($"Round: {currentRound}");
        if (currentRound % buyRound == 0)
        {
            SetBuyRound();

        }
        else
        {
            SetStandartRound();
        }

        

    }

    private void SetStandartRound()
    {
        // Deactivate turn counter
        // Call a method to show store

        remainingTurns = 10;
        tempCardLayoutHandler.PrepareNewLayout();
    }

    private void SetBuyRound()
    {
        Debug.Log("Buy round is currently in development");

        NextRound();

        // hide store if it was
        // set new layout
        // activate turn counter
        // recalculate remaining turns

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
        OnGameStartConfirm?.Invoke();
        currentRound++;
        remainingTurns = 10;
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

    private void RejectGameStart()
    {
        tempCardLayoutHandler.TakeCardsBack();
    }

    private void FinishGame()
    {

    }
}
