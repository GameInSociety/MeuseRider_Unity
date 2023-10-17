using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayGlobalScore : MonoBehaviour {
    public static DisplayGlobalScore Instance;

    public Score score;

    public Text uiText_HighScore;
    public Text uiText_Score;
    public Text uiText_Madeleines;
    public Text uiText_Bries;
    public Text uiText_Mirabelles;
    public Text uiText_Dragees;
    public Text uiText_Boosts;

    private void Awake()
    {
        Instance = this;
        score.Load();
        UpdateUI();
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI() {
        uiText_HighScore.text = "" + PlayerPrefs.GetInt("highscore", 0);
        uiText_Score.text = "" + score.total;

        uiText_Mirabelles.text = "x " + score.mirabelles.ToString();
        uiText_Bries.text = "x " + score.bries.ToString();
        uiText_Madeleines.text = "x " + score.madeleines.ToString();
        uiText_Dragees.text = "x " + score.dragees.ToString();
        uiText_Boosts.text = "x " + score.boosts.ToString();
    }
}
