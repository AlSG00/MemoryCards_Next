using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEW_GameProgression : MonoBehaviour
{
    // Depends on current round and maybe smth else
    // Will specify current set of layouts, random events and cards
    public enum GameStage
    {
        Easy,
        Medium,
        Hard,
        VeryHard
    }

    public static GameStage stage;

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
    public static event System.Action<int> OnNextRound;
    public static event System.Action/*<bool>*/ FirstTimePlaying;

    public delegate void TurnAction(bool decreased, int changeValue = 1);
    public static event TurnAction OnTurnsChanged;

    public bool firstTimePlaying;
    public bool isTurnCounterActive;

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

    private void Start()
    {
        if (firstTimePlaying)
        {
            FirstTimePlaying?.Invoke();
        }
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
        OnTurnsChanged?.Invoke(true);
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
        OnNextRound?.Invoke(currentRound);
        Debug.Log($"Round: {currentRound}");
        if (currentRound % buyRound == 0)
        {
            SetBuyRound();
        }
        else
        {
            SetStandartRound();
        }

        // TODO: Прикрутить, чтобы число ходов вычислялось по какой-нибудь формуле

    }

    private void SetStandartRound()
    {
        tempCardLayoutHandler.PrepareNewLayout();
    }

    private void SetBuyRound()
    {
        // Deactivate turn counter
        // Call a method to show store
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

        //OnTurnsChanged?.Invoke(remainingTurns);

        tempCardLayoutHandler.PrepareNewLayout();
        OnNextRound?.Invoke(currentRound);
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
