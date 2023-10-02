using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayScore : MonoBehaviour
{
    public static DisplayScore Instance;

    public Text uiText;

    float timer = 0f;
    public float displayDuration = 2f;
    bool displaying = false;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        transform.localScale = Vector3.zero;

        uiText.text = DisplayGlobalScore.score.ToString();

    }

    private void Update()
    {
        if (displaying)
        {
            timer += Time.deltaTime;
            if (timer > displayDuration)
            {
                displaying = false;
                transform.DOScale(0f, 0.5f).SetEase(Ease.InBounce);
            }
        }
    }

    public void AddScore()
    {
        Debug.Log("score");
        DisplayGlobalScore.score += 50;

        transform.localScale = Vector3.zero;

        CancelInvoke("AddScoreDelay");
        Invoke("AddScoreDelay", 1.5f);

        transform.DOScale(1f, 0.5f).SetEase(Ease.OutBounce);

        

    }

    void AddScoreDelay()
    {
        uiText.text = DisplayGlobalScore.score.ToString();

        Tween.Bounce(transform);

        displaying = true;
        timer = 0f;
    }
}
