using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionProgressHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TimeLineHandler _timeLineHandler;
    [SerializeField] private RandomSoundGenerator _soundGenerator;
    [SerializeField] private CardLayoutHandler _cardLayoutHandler;
    [SerializeField] private CardGenerator _cardGenerator;
    [SerializeField] private Highscore _highscore;

    [Header("Animation")]
    [SerializeField] private Animator _deathAnimator;
    [SerializeField] private Animator _menuAnimator;

    [Header("Audio")]
    [SerializeField] private AudioSource _cardPackAudioSource;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _secondMusicSource;
    [SerializeField] private AudioSource _whisperSource;

    [Header("Game logic parameters")]
    public bool isGamePaused;
    public bool counterStarted = false;
    public bool gameEnded = false;
    [HideInInspector] public bool firstDarkCardMet = false;
    [HideInInspector] public bool firstStrangeCardMet = false;

    [Header("Score parameters")]
    [HideInInspector] public int currentRound = 0;
    [SerializeField] private int[] _pointsForEachRound = { 30, 500, 3000, 5000 };
    private int _score = 0;

    [Header("Time parameters")]
    public int roundToactivateTimer = 3;
    [HideInInspector] public float timeStartDecreaseValue;
    [HideInInspector] private float timeStartDecreaseDebuff;
    [SerializeField] private float _timeRemaining;
    [SerializeField] private float _timeDecreaseValue = 0.01f;
    [SerializeField] private float _timeDecreaseDebuff = 0.001f;
    [SerializeField] private float _maxTime = 30f;
    
    public delegate void Action(int score);
    public static event Action onScoreChanged;

    private void Start()
    {
        timeStartDecreaseValue = _timeDecreaseValue;
        timeStartDecreaseDebuff = _timeDecreaseDebuff;
        _timeLineHandler.Hide();
        _timeRemaining = _maxTime;
    }

    private void FixedUpdate()
    {
        if (!isGamePaused &&
            counterStarted &&
            !gameEnded)
        {
            _timeRemaining -= _timeDecreaseValue;

            if (_timeRemaining <= 0)
            {
                _timeRemaining = 0;
                gameEnded = true;
                FinishGame();
            }

            _timeLineHandler.Setvalue(_timeRemaining);

            if (_timeRemaining < _maxTime / 2)
            {
                _whisperSource.volume = (1 - ((1 / (_maxTime / 2) * _timeRemaining))) / 10;
            }
            else
            {
                _whisperSource.volume = 0;
            }
        }
    }

    public void AddScore(int score)
    {
        _score += score;
        onScoreChanged?.Invoke(_score);
        _highscore.Set(_score);
    }

    public void EraseScore()
    {
        _highscore.Set(_score);
        _score = 0;
        SetRound();
        onScoreChanged?.Invoke(_score);
    }

    public void SetRound()
    {
        int round = 0;

        for (int i = 0; i < _pointsForEachRound.Length; i++)
        {
            if (_score >= _pointsForEachRound[i])
            {
                round++;
            }
        }

        if (currentRound != round)
        {
            if (round != 0)
            {
                _cardPackAudioSource.PlayOneShot(_cardPackAudioSource.clip);
            }
            _soundGenerator.SetCurrentClipList(round);
        }
        currentRound = round;

        if (currentRound >= roundToactivateTimer)
        {
            _timeLineHandler.SetMaxValue(_maxTime);
            _timeLineHandler.Setvalue(_maxTime);
            _timeLineHandler.Show();
            counterStarted = true;
        }
    }

    public void ResetProgress()
    {
        currentRound = 0;
        _timeRemaining = _maxTime;
        isGamePaused = false;
        counterStarted = false;
        gameEnded = false;
        firstDarkCardMet = false;
        firstStrangeCardMet = false;
        EraseScore();
        _timeLineHandler.Hide();
        _cardGenerator.RemoveAllCards();
        _soundGenerator.SetCurrentClipList(0);
        _soundGenerator.RandomAudioSource.Stop();
        SetRound();
    }

    public void ResetSound()
    {
        _musicSource.Stop();
        _secondMusicSource.Stop();
        _soundGenerator.RandomAudioSource.Stop();
        _whisperSource.volume = 0;
    }

    public void AddCancelDebuff()
    {
        if (counterStarted)
        {
            _timeDecreaseValue += _timeDecreaseDebuff;
            _timeLineHandler.IndicateDebuff();
        }
    }

    public void AddUnpickDebuff()
    {
        if (counterStarted)
        {
            _timeDecreaseValue += _timeDecreaseDebuff / 2;
            _timeLineHandler.IndicateDebuff();
        }
    }

    public void AddTime(int time)
    {
        _timeRemaining += time;

        if (_timeRemaining > _maxTime)
        {
            _timeRemaining = _maxTime;
        }
    }

    public void ResetDebuff()
    {
        _timeDecreaseValue = timeStartDecreaseValue;
        if (currentRound != 0)
        {
            _timeLineHandler.IndicateDebuffReset();
        }
    }


    public void FinishGame()
    {
        _cardLayoutHandler.ActivateCardColliders(false);
        _deathAnimator.SetTrigger("died");
        _highscore.Set(_score);
    }

    public void ActivateDeathMenu()
    {
        _menuAnimator.SetTrigger("death");
    }


    #region Scripted events

    public void StrangeCardMet()
    {
        firstStrangeCardMet = true;
        _musicSource.Play();
    }

    public void DarkCardMet()
    {
        firstDarkCardMet = true;
        _secondMusicSource.Play();
    }

    #endregion
}
