using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        NEW_GameProgression.onScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        NEW_GameProgression.onScoreChanged -= UpdateScore;
    }

    public void UpdateScore(int currentScore)
    {
        _scoreText.text = $"{currentScore.ToString()}";
    }
}
