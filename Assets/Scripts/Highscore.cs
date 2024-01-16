using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "Highscore")]
public class Highscore : ScriptableObject
{
    public int highscore;

    public delegate void highscoreAction(int newScore);
    public static event highscoreAction onHighscoreChanged;

    public void Set(int newScore)
    {
        if (highscore < newScore)
        {
            highscore = newScore;
            onHighscoreChanged?.Invoke(newScore);
        }
    }

    public int Get()
    {
        return highscore;
    }
}
