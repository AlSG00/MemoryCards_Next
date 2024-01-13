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
        FullRandom
    }

    public enum RoundType
    {
        Confirm,
        Tutorial,
        Standart
    }

    public static GameStage stage;

    public int easyDifficultyRound;
    public int mediumDifficultyRound;
    public int hardDifficultyRound;
    public int veryHardDifficultyRound;

    public NEW_CardGenerator tempCardGenerator;
    public NEW_CardLayoutHandler tempCardLayoutHandler;

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
    public static event System.Action<int> OnShowHint; // 0 -  hide all
    public static event System.Action<int> OnNextRound;
    public static event System.Action FirstTimePlaying;

    //public static event System.Action<int> OnStartTutorialPhase;
    public static event System.Action<bool> OnStartBuyRound; 

    public static event System.Action<bool> OnActivateTurnCounter;
    public static event System.Action<bool> OnActivateScoreList;
    public static event System.Action<MoneyRopeHandler.Visibility> OnActivateMoneyRope;
    //public static event System.Action<MoneyRopeHandler.Visibility> OnFullyActivateMoneyRope;
    public static event System.Action<int> OnAddMoney;
    public static event System.Action<int> onScoreChanged;

    public delegate void TurnAction(bool decreased, int changeValue = 1);
    public static event TurnAction OnTurnsChanged;


    public bool firstTimePlaying; // TODO: Save this parameter to JSON
    public bool tutorialComplete;
    public bool playingTutorial;
    public int _tutorialProgress;


    public bool isTurnCounterActive;
    public bool isScoreListActive;
    public bool isStopwatchActive;
    public bool isMoneyRopeActive;
    public bool isBuyRoundGoing;

    private void OnEnable()
    {
        StartButton.OnGameStart += StartGame;
        RejectStartButton.OnGameStartReject += RejectGameStart;
        CardComparator.OnPickConfirm += CheckRoundProgression;
        ScaleContinue.OnContinueGame += NextRound;
        ScaleExit.OnFinishGame += FinishGameTest;
    }

    private void OnDisable()
    {
        StartButton.OnGameStart -= StartGame;
        RejectStartButton.OnGameStartReject -= RejectGameStart;
        CardComparator.OnPickConfirm -= CheckRoundProgression;
        ScaleContinue.OnContinueGame -= NextRound;
        ScaleExit.OnFinishGame -= FinishGameTest;
    }

    private void Start()
    {
        isTurnCounterActive = false;
        isScoreListActive = false;
        isStopwatchActive = false;

        if (tutorialComplete == false)
        {
            playingTutorial = true;
        }
        stage = GameStage.VeryEasy;
        if (firstTimePlaying)
        {
            FirstTimePlaying?.Invoke();
        }
    }

    private void CheckRoundProgression(List<GameObject> confirmedCards)
     {
        OnShowHint?.Invoke(0);

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
                   
                    //OnPlayTutorial?.Invoke(10);
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
            //money += 1; //TODO: TEMP. Move to money script

            OnAddMoney?.Invoke(1);
            onScoreChanged?.Invoke(score);

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
                OnShowHint?.Invoke(1);
                break;

            case 3:
                EnableScoreList(true);
                OnShowHint?.Invoke(2);
                break;
                // TODO: Add new tutorial phase here
            case 6:
                EnableTurnCounter(true);
                OnShowHint?.Invoke(3);
                break;

            case 9:
                // Hints only
                OnShowHint?.Invoke(4);
                break;
        }
    }

    private void NextRound()
    {
        if (isTurnCounterActive == false)
        {
            EnableTurnCounter(true);
        }

        if (isScoreListActive == false)
        {
            EnableScoreList(true);
        }

        if (currentRound % buyRound == 0 &&
            isBuyRoundGoing == false)
        {
            SetBuyRound();
        }
        else
        {
            currentRound++;
            OnNextRound?.Invoke(currentRound);
            SetStandartRound();
        }
    }

    private void FinishGameTest()
    {
        Debug.Log("Game finished");
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

    private void EnableMoneyRope(MoneyRopeHandler.Visibility visibility)
    {
        if (visibility.Equals(MoneyRopeHandler.Visibility.Visible))
        {
            isMoneyRopeActive = true;
        }

        // isMoneyRopeActive = isEnabled;
        OnActivateMoneyRope?.Invoke(visibility);
         //OnActivateMoneyRope?.Invoke(isEnabled);
    }

    private void SetStandartRound()
    {
        if (isBuyRoundGoing)
        {
            isBuyRoundGoing = false;
            OnStartBuyRound?.Invoke(false);
            EnableTurnCounter(true);
            EnableMoneyRope(MoneyRopeHandler.Visibility.PartiallyVisible);
        }

        UpdateDifficulty();
        tempCardLayoutHandler.PrepareNewLayout();
    }

    private void SetBuyRound()
    {
        isBuyRoundGoing = true;
        EnableTurnCounter(false);
        EnableMoneyRope(MoneyRopeHandler.Visibility.Visible);
        OnStartBuyRound?.Invoke(true);

        if (firstTimePlaying)
        {
            OnShowHint?.Invoke(5);
        }
        //Debug.Log("Buy round is currently in development");
        //NextRound();
    }

    private void UpdateDifficulty()
    {
        if (currentRound < easyDifficultyRound)
        {
            stage = GameStage.Easy;
        }
        else if (currentRound < mediumDifficultyRound)
        {
            stage = GameStage.Medium;
        }
        else if (currentRound < hardDifficultyRound)
        {
            stage = GameStage.Hard;
        }
        else if (currentRound < veryHardDifficultyRound)
        {
            stage = GameStage.VeryHard;
        }
        else
        {
            stage = GameStage.FullRandom;
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

        if (firstTimePlaying && tutorialComplete == false)
        {
            StartTutorial();
        }
        else
        {
            UpdateDifficulty();
            EnableScoreList(true);
            EnableTurnCounter(true);
            EnableMoneyRope(MoneyRopeHandler.Visibility.PartiallyVisible);
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
