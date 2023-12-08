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
        UpdateUI();
    }

    private void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI() {
        score.Load();

        uiText_HighScore.text = "" + PlayerPrefs.GetInt("highscore", 0);
        uiText_Score.text = "" + score.total;

        uiText_Mirabelles.text = $"{score.mirabelles} mirabelles";
        uiText_Bries.text = $"{score.bries} bries";
        uiText_Madeleines.text = $"{score.madeleines} madeleines";
        uiText_Dragees.text = $"{score.mirabelles} drag�es";
        uiText_Boosts.text = score.boosts.ToString();
    }
}
