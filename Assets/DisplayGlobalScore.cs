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

    float deltaTime;
    private void Update()
    {
        uiText_HighScore.text = "" + highScore;

        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        //uiText_Score.text = "" + score;

        uiText_Score.text = "" + fps;
    }
}
