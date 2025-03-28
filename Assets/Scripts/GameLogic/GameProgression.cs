using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameProgression : MonoBehaviour
{
    // Depends on current round and maybe smth else
    // Will specify current set of layouts, random events and cards

    [Header("Game stage parameters")]
    public static Difficulty StartCardDifficulty;
    public static Difficulty StartLayoutDifficulty;
    public static Difficulty CardDifficulty;
    public static Difficulty LayoutDifficulty;

    public bool firstTimePlaying; // TODO: Save this parameter to JSON

    [SerializeField] private CardGenerator _cardGenerator;
    [SerializeField] private CardLayoutHandler _cardLayoutHandler;

    public bool tutorialComplete;
    public bool playingTutorial;
    public int _tutorialProgress;
    private bool _isGameGoing;
    public bool isTurnCounterActive;
    public bool isScoreListActive;
    public bool isStopwatchActive;
    public bool isChestActive;
    public bool isMoneyRopeActive;
    public bool isBuyRoundGoing;
    public bool IsGameLost;
    //private bool _isGamePaused;

    public int currentRound = 0;
    public int score = 0;
    [Tooltip("Each round dividible by this digit will be a buy round")]
    [Range(1f, 1000f)] public int buyRound;
    [Range(1f, 1000f)] public int switchDifficultyRound;
    public System.Diagnostics.Stopwatch ElapsedPlayTime = new System.Diagnostics.Stopwatch();
    [SerializeField] private int _stopwatchActivationChance;
    [SerializeField] private int _chestActivateChance;

    public delegate void TurnAction(bool decreased, int changeValue = 1);

    public static event TurnAction OnTurnsChanged;
    public static event System.Action<int, int> OnPlayTutorial;
    public static event System.Action<int> OnShowHint; // 0 -  hide all
    public static event System.Action<int> OnNextRound;
    public static event System.Action FirstTimePlaying;
    public static event System.Action<bool> OnStartBuyRound;
    public static event System.Action<bool> OnActivateTurnCounter;
    public static event System.Action<bool> OnActivateScoreList;
    public static event System.Action<bool, int> ActivateStopwatch;
    public static event System.Action<bool> ActivateChest;
    public static event System.Action DeactivateStopwatch;
    public static event System.Action<MoneyRopeHandler.Visibility> OnActivateMoneyRope;
    public static event System.Action<int> AddCurrentMoney;
    public static event System.Action ResetCurrentMoney;
    public static event System.Action<int> onScoreChanged;
    public static event System.Action<bool> PauseGame;
    public static event System.Action OnGameFinished;
    public static event System.Action LoseGame;

    public enum Difficulty
    {
        VeryEasy,
        Easy,
        Medium,
        Hard,
        VeryHard,
        Random
    }

    public enum RoundType
    {
        Confirm,
        Tutorial,
        Standart
    }

    public static bool IsGamePaused { get; private set; }

    private void OnEnable()
    {
        SetDifficultyButton.DifficultyPicked += StartGame;
        RejectStartButton.OnGameStartReject += RejectGameStart;
        RejectStartButton.OnGameStartReject += ClearData;
        CardComparator.OnPickConfirm += CheckRoundProgression;
        ScaleContinue.OnContinueGame += SetNextRound;
        ScaleExit.OnFinishGame += FinishGameOnBuyRound;
        ScaleSuspend.OnSuspendGame += SaveAndClearData;
        BackToMenuButton.ReturningToMainMenu += ClearData;
        RemainingTurnsHandler.OutOfTurns += OnLoseGame;
        Stopwatch.OutOfTime += OnLoseGame;
        PlayerInput.EscapeButtonPressed += SetGamePause;
        ContinueGameButton.ContinueButtonClicked += SetGamePause;
    }

    private void OnDisable()
    {
        SetDifficultyButton.DifficultyPicked -= StartGame;
        RejectStartButton.OnGameStartReject -= RejectGameStart;
        RejectStartButton.OnGameStartReject -= ClearData;
        CardComparator.OnPickConfirm -= CheckRoundProgression;
        ScaleContinue.OnContinueGame -= SetNextRound;
        ScaleExit.OnFinishGame -= FinishGameOnBuyRound;
        ScaleSuspend.OnSuspendGame -= SaveAndClearData;
        BackToMenuButton.ReturningToMainMenu -= ClearData;
        RemainingTurnsHandler.OutOfTurns -= OnLoseGame;
        Stopwatch.OutOfTime += OnLoseGame;
        PlayerInput.EscapeButtonPressed -= SetGamePause;
        ContinueGameButton.ContinueButtonClicked -= SetGamePause;
    }

    private void Start()
    {
        isTurnCounterActive = false;
        isScoreListActive = false;
        isStopwatchActive = false;
        IsGameLost = false;
        IsGamePaused = false;

        if (tutorialComplete == false)
        {
            playingTutorial = true;
        }

        LayoutDifficulty = Difficulty.VeryEasy;
        CardDifficulty = StartCardDifficulty;

        if (firstTimePlaying)
        {
            FirstTimePlaying?.Invoke();
        }

        score = 0;

        ElapsedPlayTime.Reset();

        onScoreChanged?.Invoke(score);
    }

    private void CheckRoundProgression(List<GameObject> confirmedCards)
    {
        OnShowHint?.Invoke(0);
        // decrease remaining turns
        //if (currentRound == 0 && confirmedCards != null)
        //{
        //    _cardLayoutHandler.RemoveCertainCards(confirmedCards);
        //    ConfirmGameStart();
        //    return;
        //}

        if (confirmedCards == null)
        {
            OnTurnsChanged?.Invoke(true);
            return;
        }

        // TODO: Rework this block
        /// Tutorial code block
        /*
        if (playingTutorial)
        {
            _cardLayoutHandler.RemoveCertainCards(confirmedCards);
            if (_cardGenerator.CheckRemainingCards() == false)
            {
                _tutorialProgress++;
                Debug.Log(_tutorialProgress);
                UpdateTutorialProgression();
                OnPlayTutorial?.Invoke(_tutorialProgress, currentRound);
                if (_tutorialProgress == 9) // TODO: is it ok? No it's not
                {
                    playingTutorial = false;
                }
            }
            else
            {
                if (isTurnCounterActive)
                {
                    OnTurnsChanged?.Invoke(true);
                }
            }
        }
        else
        {
         */
        ///
            score += 10; //TODO: TEMP. Move to score script

            onScoreChanged?.Invoke(score);
            _cardLayoutHandler.RemoveCertainCards(confirmedCards);
            if (_cardGenerator.CheckRemainingCards() == false)
            {
                int moneyToAdd = 1 + (int)CardDifficulty;
                AddCurrentMoney?.Invoke(moneyToAdd); // TODO: Rework
                SetNextRound();
            }
            else
            {
                if (isTurnCounterActive)
                {
                    OnTurnsChanged?.Invoke(true);
                }
            }
        //}
    }

    private void SetGamePause()
    {
        if (_isGameGoing == false)
        {
            return;
        }

        IsGamePaused = !IsGamePaused;
        PauseGame?.Invoke(IsGamePaused);
    }

    private void UpdateTutorialProgression()
    {
        switch (_tutorialProgress)
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

    private void EnableTurnCounter(bool setEnabled)
    {
        isTurnCounterActive = setEnabled;
        OnActivateTurnCounter?.Invoke(setEnabled);
    }

    private void EnableScoreList(bool setEnabled)
    {
        isScoreListActive = setEnabled;
        OnActivateScoreList?.Invoke(setEnabled);
    }

    private void EnableStopwatch(bool setEnabled, int timeInSeconds = 0)
    {
        isStopwatchActive = setEnabled;
        ActivateStopwatch?.Invoke(setEnabled, timeInSeconds);
    }

    private void EnableChest(bool setEnabled)
    {
        isChestActive = setEnabled;
        ActivateChest?.Invoke(setEnabled);
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

    #region ROUND INITIALIZATION METHODS

    private void SetNextRound()
    {
        EnableStopwatch(false);
        EnableChest(false);

        if (isScoreListActive == false)
        {
            EnableScoreList(true);
        }

        if (currentRound != 0 &&
            currentRound % buyRound == 0 &&
            isBuyRoundGoing == false)
        {
            SetBuyRound();
        }
        else
        {
            currentRound++;
            OnNextRound?.Invoke(currentRound);
            SetStandartRound();
            //EnableStopwatch(true, 30);
        }
    }

    private void SetStandartRound()
    {
        ElapsedPlayTime.Start();
        EnableTurnCounter(true);
        if (isBuyRoundGoing)
        {
            isBuyRoundGoing = false;
            OnStartBuyRound?.Invoke(false);
            EnableMoneyRope(MoneyRopeHandler.Visibility.PartiallyVisible);
        }
        SetRoundMods();
        UpdateDifficulty();
        _cardLayoutHandler.PrepareNewLayout(currentRound);
    }

    private void SetBuyRound()
    {
        ElapsedPlayTime.Stop();

        isBuyRoundGoing = true;
        EnableTurnCounter(false);
        EnableMoneyRope(MoneyRopeHandler.Visibility.Visible);
        OnStartBuyRound?.Invoke(true);

        if (firstTimePlaying)
        {
            OnShowHint?.Invoke(5);
        }
    }

    private void SetRoundMods()
    {
        int stopwatchAddChance = UnityEngine.Random.Range(0, 100);
        int chestAddChance = UnityEngine.Random.Range(0, 100);

        Debug.Log($" stopwatch : chest | {stopwatchAddChance} : {chestAddChance}");
        if (stopwatchAddChance < _stopwatchActivationChance)
        {
            ActivateStopwatch(true, 30); // TODO: Calculate remaining time
        }

        if (chestAddChance < _chestActivateChance)
        {
            ActivateChest(true);
        }

        else
        {
            return;
        }
    }

    // TODO: Redo
    private void UpdateDifficulty()
    {
        if ((currentRound - 1) == 0)
        {
            return;
        }

        if ((currentRound - 1) % switchDifficultyRound != 0)
        {
            return;
        }

        if (LayoutDifficulty == Difficulty.Random &&
            CardDifficulty == Difficulty.Random)
        {
            return;
        }

        LayoutDifficulty++;
        if (LayoutDifficulty == Difficulty.Random)
        {
            LayoutDifficulty = StartLayoutDifficulty;
            CardDifficulty++;
        }
    }

    #endregion

    #region START GAME
    private void StartGame()
    {
        LayoutDifficulty = Difficulty.VeryEasy;
        CardDifficulty = StartCardDifficulty;
        ClearData();
        ConfirmGameStart();
    }

    private void ConfirmGameStart()
    {
        _isGameGoing = true;
        ElapsedPlayTime.Start();
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
            _cardLayoutHandler.PrepareNewLayout(currentRound);
            OnNextRound?.Invoke(currentRound);
        }
    }

    private void StartTutorial()
    {
        _tutorialProgress = 0;
        UpdateTutorialProgression();
        OnPlayTutorial?.Invoke(_tutorialProgress, currentRound);
    }

    private void RejectGameStart()
    {
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

    private void FinishGameOnBuyRound()
    {
        _isGameGoing = false;
        isBuyRoundGoing = false;
        OnStartBuyRound?.Invoke(false);
        EnableTurnCounter(false);
        EnableScoreList(false);
        EnableMoneyRope(MoneyRopeHandler.Visibility.Hidden);
        OnGameFinished?.Invoke();
    }

    private void OnLoseGame()
    {
        _isGameGoing = false;
        IsGameLost = true;
        LoseGame?.Invoke();
        ElapsedPlayTime.Stop();
        EnableScoreList(false);
        EnableMoneyRope(MoneyRopeHandler.Visibility.Hidden);
        EnableTurnCounter(false);
        EnableStopwatch(false);
        EnableChest(false);
    }

    private void ClearData()
    {
        _isGameGoing = false;
        IsGameLost = false;
        IsGamePaused = false;
        currentRound = 0;
        score = 0;
        ElapsedPlayTime.Reset();
        onScoreChanged?.Invoke(score);
        OnStartBuyRound?.Invoke(false);
        ResetCurrentMoney?.Invoke();
        EnableScoreList(false);
        EnableTurnCounter(false);
        EnableMoneyRope(MoneyRopeHandler.Visibility.Hidden);
    }

    private void SaveAndClearData()
    {
        _isGameGoing = false;
        // TODO: save progress - score, current round, current difficulty
        ClearData();
    }
}
