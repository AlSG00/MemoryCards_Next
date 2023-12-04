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
    public static event System.Action<int> OnPlayTutorial;
    public static event System.Action<int> OnTutorialProgress;
    public static event System.Action<int> OnNextRound;
    public static event System.Action FirstTimePlaying;

    public static event System.Action<int> OnStartTutorialPhase;


    public static event System.Action<bool> OnActivateTurnCounter;
    public static event System.Action<bool> OnActivateScoreList;

    public delegate void TurnAction(bool decreased, int changeValue = 1);
    public static event TurnAction OnTurnsChanged;


    public bool firstTimePlaying; // TODO: Save this parameter to JSON
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

        if (isTurnCounterActive)
        {
            OnTurnsChanged?.Invoke(true);
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
                UpdateTutorialProgression();
                OnPlayTutorial?.Invoke(_tutorialProgress);

                if (_tutorialProgress == 9) // TODO: is it ok?
                {
                    playingTutorial = false;
                }
            }
        }
        else
        {
            // it's null if wrong pair or card was unpicked
            if (confirmedCards == null)
            {
                return;
            }

            score += 10; //TODO: TEMP. Move to score script
            money += 1; //TODO: TEMP. Move to money script

            Debug.Log($"Money:{money}");
            tempCardLayoutHandler.RemoveCertainCards(confirmedCards);
            if (tempCardGenerator.CheckRemainingCards() == false)
            {
                NextRound();
            }
        }
    }

    private void UpdateTutorialProgression()
    {
        switch(_tutorialProgress)
        {
            case 0:
                // Hints only
                OnStartTutorialPhase?.Invoke(1);
                break;

            case 3:
                EnableScoreList(true);
                OnStartTutorialPhase?.Invoke(2);
                break;

            case 6:
                EnableTurnCounter(true);
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
        if (isTurnCounterActive == false)
        {
            EnableTurnCounter(true);
        }

        if (currentRound % buyRound == 0)
        {
            SetBuyRound();
        }
        else
        {
            SetStandartRound();
        }
    }

    private void EnableTurnCounter(bool isEnabled)
    {
        isTurnCounterActive = isEnabled;
        OnActivateTurnCounter?.Invoke(isEnabled);
        
    }

    private void EnableScoreList(bool isEnabled)
    {
        isScoreListActive = isEnabled;
        OnActivateScoreList?.Invoke(isEnabled);
    }

    private void SetStandartRound()
    {

        UpdateDifficulty();
        tempCardLayoutHandler.PrepareNewLayout();
    }

    private void SetBuyRound()
    {
        EnableTurnCounter(false);
        Debug.Log("Buy round is currently in development");
        NextRound();
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
            
            EnableScoreList(true);
            EnableTurnCounter(true);
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
        OnPlayTutorial?.Invoke(_tutorialProgress);
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
