using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _highscore;
    [SerializeField] private Highscore _highscoreData;

    private void OnEnable()
    {
        Highscore.onHighscoreChanged += UpdateHighscore;
    }

    private void OnDisable()
    {
        Highscore.onHighscoreChanged -= UpdateHighscore;
    }

    private void Start()
    {
        _highscoreData.highscore = PlayerPrefs.GetInt("highscore");
        UpdateHighscore(_highscoreData.highscore);
    }
    
    public void UpdateHighscore(int newScore)
    {
        _highscore.text = $"Highscore: {_highscoreData.highscore.ToString()}";
        PlayerPrefs.SetInt("highscore", _highscoreData.highscore);
    }
}
