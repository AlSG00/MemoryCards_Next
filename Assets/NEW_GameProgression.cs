using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEW_GameProgression : MonoBehaviour
{
    // Depends on current round and maybe smth else
    // Will specify current set of layouts, random events and cards
    public enum GameStage
    {
        VeryEasy,
        Easy,
        Medium,
        Hard,
        VeryHard,
    }

    public enum RoundType
    {
        Confirm,
        Tutorial,
        Standart
    }

    public static GameStage stage;

    public NEW_CardGenerator tempCardGenerator;
    public NEW_CardLayoutHandler tempCardLayoutHandler;

    //public int remainingCards;
    public int currentRound = 0;
    public int remainingTurns = 0;
    public int score = 0;
    public int money = 0; // available only in this session
    public int mainMoney = 0; // can be used in upgrade store

    [Tooltip("Each round dividible by this digit will be a buy round")]
    public int buyRound;

    public static event System.Action OnPressStart;
    public static event System.Action OnGameStartConfirm;
    public static event System.Action<int> OnTutorialStart;
    public static event System.Action<int> OnTutorialProgress;
    public static event System.Action<int> OnNextRound;
    public static event System.Action/*<bool>*/ FirstTimePlaying;

    // To activate text hints
    //public static event System.Action OnFirstTutorialPhase;
    //public static event System.Action OnSecondTutorialPhase;
    //public static event System.Action OnThirdTutorialPhase;
    //public static event System.Action OnFourthTutorialPhase;
    public static event System.Action<int> OnStartTutorialPhase;


    public static event System.Action<bool> OnActivateTurnCounter;
    public static event System.Action<bool> OnActivateScoreList;

    public delegate void TurnAction(bool decreased, int changeValue = 1);
    public static event TurnAction OnTurnsChanged;


    public bool firstTimePlaying;
    public bool playingTutorial;
    private int _tutorialProgress;


    public bool isTurnCounterActive;
    public bool isScoreListActive;
    public bool isStopwatchActive;

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
        isTurnCounterActive = false;
        isScoreListActive = false;
        isStopwatchActive = false;

        if (firstTimePlaying)
        {
            stage = GameStage.VeryEasy;
            playingTutorial = true;
            FirstTimePlaying?.Invoke();
        }
    }

    private void CheckRoundProgression(List<GameObject> confirmedCards/*, RoundType roundType*/)
     {
        // decrease remaining turns
        if (currentRound == 0 && confirmedCards != null)
        {
            tempCardLayoutHandler.RemoveCertainCards(confirmedCards);
            ConfirmGameStart();
            return;
        }

        if (playingTutorial)
        {
            if (confirmedCards == null)
            {
                return;
            }
            tempCardLayoutHandler.RemoveCertainCards(confirmedCards);
            if (tempCardGenerator.CheckRemainingCards() == false)
            {
                _tutorialProgress++;
                Debug.Log(_tutorialProgress);
                OnTutorialStart?.Invoke(_tutorialProgress);
                UpdateTutorialProgression();
            }
        }

        if (isTurnCounterActive)
        {
            OnTurnsChanged?.Invoke(true);
        }
        
        // it's null if wrong pair or card was unpicked
        if (confirmedCards == null)
        {
            return;
        }

        score += 10; //TODO: TEMP. Move to score script
        money += 1;
        Debug.Log($"Money:{money}");
        tempCardLayoutHandler.RemoveCertainCards(confirmedCards);
        if (tempCardGenerator.CheckRemainingCards() == false)
        {
            NextRound();
        }
    }

    private void UpdateTutorialProgression()
    {
        switch(_tutorialProgress)
        {
            case 0:
                // Hints only
                OnStartTutorialPhase?.Invoke(1);
                //OnFirstTutorialPhase?.Invoke(); это не нужно, первая подсказка будет перенесена на ивент начала туториала
                break;

            case 3:
                isScoreListActive = true;
                OnActivateScoreList?.Invoke(true);
                OnStartTutorialPhase?.Invoke(2);
                break;

            case 6:
                isTurnCounterActive = true;
                OnActivateTurnCounter?.Invoke(true);
                OnStartTutorialPhase?.Invoke(3);
                break;

            case 9:
                // Hints only
                OnStartTutorialPhase?.Invoke(4);
                break;
        }
    }

    private void NextRound()
    {
        currentRound++;
        OnNextRound?.Invoke(currentRound);

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

        UpdateDifficulty();
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

    private void UpdateDifficulty()
    {
        if (currentRound < 2)
        {
            stage = GameStage.Easy;
        }
        else if (currentRound < 3)
        {
            stage = GameStage.Medium;
        }
        else if (currentRound < 4)
        {
            stage = GameStage.Hard;
        }
        else /*if (currentRound < 20)*/
        {
            stage = GameStage.VeryHard;
        }
        Debug.Log($"Stage: {stage}");
    }

    #region START GAME
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

        if (firstTimePlaying)
        {
            StartTutorial();
        }
        else
        {
            tempCardLayoutHandler.PrepareNewLayout();
            OnNextRound?.Invoke(currentRound);
        }
        
        // reset score
        // reset all debuffs
        // reset all items
        // reset turn counter
        // reset stopwatch
        // reset all cards if has any
        // reset rounds
        // reset card layout
    }

    private void StartTutorial()
    {
        _tutorialProgress = 0;
        UpdateTutorialProgression();
        OnTutorialStart?.Invoke(_tutorialProgress);
       // OnStartTutorialPhase?.Invoke(1);
    }

    private void RejectGameStart()
    {
        tempCardLayoutHandler.TakeCardsBack();
    }
    #endregion

    private void FinishGame()
    {

    }
}
