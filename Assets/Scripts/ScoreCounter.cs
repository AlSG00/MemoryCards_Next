using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void OnEnable()
    {
        GameProgression.onScoreChanged += UpdateScore;
    }

    private void OnDisable()
    {
        GameProgression.onScoreChanged -= UpdateScore;
    }

    public void UpdateScore(int currentScore)
    {
        _scoreText.text = currentScore.ToString();
    }
}
