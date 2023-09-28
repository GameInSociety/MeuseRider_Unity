using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGlobalScore : MonoBehaviour
{
    public int highScore = 0;
    public static int score;
    public static DisplayGlobalScore Instance;

    public Text uiText_HighScore;
    public Text uiText_Score;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        highScore = PlayerPrefs.GetInt("highscore", 0);

        if ( score > highScore )
        {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);
        }

    }

    private void Update()
    {
        uiText_HighScore.text = "" + highScore;
        uiText_Score.text = "" + score;
    }
}
