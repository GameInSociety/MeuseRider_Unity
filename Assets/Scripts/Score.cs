using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

[System.Serializable]
public class Score
{
    public int madeleines = 0;
    public int bries = 0;
    public int dragees = 0;
    public int mirabelles = 0;
    public int boosts = 0;

    public void Add(int a)
    {
        switch (a)
        {
            case 0:
                madeleines++;
                break;
            case 1:
                bries++;
                break;
            case 2:
                dragees++;
                break;
            case 3:
                mirabelles++;
                break;
            case 4:
                boosts++;
                break;
            default:
                break;
        }

        int highScore = PlayerPrefs.GetInt("highscore", 0);

        if ( total >= highScore)
        {
            PlayerPrefs.SetInt("highscore", total);
        }
    }

    public int total
    {
        get
        {
            int i = madeleines + bries + dragees + boosts + mirabelles;
            i *= 50;
            return i;
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("madeleines", madeleines);
        PlayerPrefs.SetInt("bries", bries);
        PlayerPrefs.SetInt("dragees", dragees);
        PlayerPrefs.SetInt("mirabelles", mirabelles);
        PlayerPrefs.SetInt("boosts", boosts);

    }

    public void Load()
    {
        madeleines = PlayerPrefs.GetInt("madeleines", 0);
        bries = PlayerPrefs.GetInt("bries", 0);
        dragees = PlayerPrefs.GetInt("dragees", 0);
        mirabelles = PlayerPrefs.GetInt("mirabelles", 0);
        boosts = PlayerPrefs.GetInt("boosts", 0);
    }
}
