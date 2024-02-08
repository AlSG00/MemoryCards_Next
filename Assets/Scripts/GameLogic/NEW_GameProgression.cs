using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NEW_GameProgression : MonoBehaviour
{
    // Depends on current round and maybe smth else
    // Will specify current set of layouts, random events and cards

    [Header("Game stage parameters")]
    //public static GameStage stage;

    public static Difficulty CardDifficulty;
    public static Difficulty LayoutDifficulty;

    public bool firstTimePlaying; // TODO: Save this parameter to JSON
    public int easyDifficultyRound;
    public int mediumDifficultyRound;
    public int hardDifficultyRound;
    public int veryHardDifficultyRound;

    public NEW_CardGenerator tempCardGenerator;
    public NEW_CardLayoutHandler tempCardLayoutHandler;


    
    public bool tutorialComplete;
    public bool playingTutorial;
    public int _tutorialProgress;


    public bool isTurnCounterActive;
    public bool isScoreListActive;
    public bool isStopwatchActive;
    public bool isMoneyRopeActive;
    public bool isBuyRoundGoing;

    public int currentRound = 0;
    public int score = 0;
    public int mainMoney = 0; // can be used in upgrade store
    [Tooltip("Each round dividible by this digit will be a buy round")] public int buyRound;

    public delegate void TurnAction(bool decreased, int changeValue = 1);

    public static event TurnAction OnTurnsChanged;
    public static event System.Action OnPressStart;
    public static event System.Action OnGameStartConfirm;
    public static event System.Action<int> OnPlayTutorial;
    public static event System.Action<int> OnShowHint; // 0 -  hide all
    public static event System.Action<int> OnNextRound;
    public static event System.Action FirstTimePlaying;
    public static event System.Action OnGameFinished;
    public static event System.Action<bool> OnStartBuyRound; 
    public static event System.Action<bool> OnActivateTurnCounter;
    public static event System.Action<bool> OnActivateScoreList;
    public static event System.Action<MoneyRopeHandler.Visibility> OnActivateMoneyRope;
    public static event System.Action<int> OnAddMoney;
    public static event System.Action<int> onScoreChanged;
    public static event System.Action OnCurrentProgressReset;

    public enum Difficulty
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
        //stage = GameStage.VeryEasy;

        LayoutDifficulty = Difficulty.Easy;
        CardDifficulty = Difficulty.Easy;

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

                if (_tutorialProgress == 9) // TODO: is it ok? No it's not
                {
                    playingTutorial = false;
                }
            }
        }
        else
        {
            if (confirmedCards == null) // it's null if wrong pair or card was unpicked
            {
                return;
            }

            score += 10; //TODO: TEMP. Move to score script

            OnAddMoney?.Invoke(1); // TODO: Rework
            onScoreChanged?.Invoke(score);

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
        //if (isBuyRoundGoing == false)
        //{
        //    EnableTurnCounter(true);
        //}

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
        isBuyRoundGoing = false;
        OnStartBuyRound?.Invoke(false);
        EnableTurnCounter(true);
        EnableScoreList(true);
        OnGameFinished?.Invoke();
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
        if (visibility.Equals(MoneyRopeHandler.Visibility.Visible) ||
            visibility.Equals(MoneyRopeHandler.Visibility.PartiallyVisible))
        {
            isMoneyRopeActive = true;
        }
        else
        {
            isMoneyRopeActive = false;
        }

        OnActivateMoneyRope?.Invoke(visibility);
    }

    private void SetStandartRound()
    {
        EnableTurnCounter(true);
        if (isBuyRoundGoing)
        {
            isBuyRoundGoing = false;
            OnStartBuyRound?.Invoke(false);
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
    }

    // TODO: Redo
    private void UpdateDifficulty()
    {
        if (currentRound < easyDifficultyRound)
        {
            LayoutDifficulty = Difficulty.Easy;
            CardDifficulty = Difficulty.Easy;
        }
        else if (currentRound < mediumDifficultyRound)
        {
            LayoutDifficulty = Difficulty.Medium;
            CardDifficulty = Difficulty.Medium;
        }
        else if (currentRound < hardDifficultyRound)
        {
            LayoutDifficulty = Difficulty.Hard;
            CardDifficulty = Difficulty.Hard;
        }
        else if (currentRound < veryHardDifficultyRound)
        {
            LayoutDifficulty = Difficulty.VeryHard;
            CardDifficulty = Difficulty.VeryHard;
        }
        else
        {
            LayoutDifficulty = Difficulty.FullRandom;
            CardDifficulty = Difficulty.FullRandom;
        }
        //Debug.Log($"Stage: {stage}");
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

        if (isScoreListActive)
        {
            EnableScoreList(false);
        }

        if (isTurnCounterActive)
        {
            EnableTurnCounter(false);
        }

        if (isMoneyRopeActive)
        {
            EnableMoneyRope(MoneyRopeHandler.Visibility.Hidden);
        }
    }
    #endregion

    private void FinishGame()
    {

    }

    private void ResetCurrentProgress()
    {
        OnCurrentProgressReset?.Invoke(); 
        currentRound = 0;
        score = 0;

        // TODO: Reset money, inventory, rounds, score, difficulty
    }

    private void SaveAndClear()
    {
        // TODO: save progress - score, current round, current difficulty
    }
}
