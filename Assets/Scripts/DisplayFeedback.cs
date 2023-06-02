using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DisplayFeedback : MonoBehaviour
{
    public static DisplayFeedback Instance;

    public GameObject group;

    public Image image;
    public Text uiText;

    public Color goodColor;
    public Color badColor;

    public Outline imageOutline;
    public Outline textOutline;

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
    }

    private void Update()
    {
        if (displaying)
        {
            timer += Time.deltaTime;
            if ( timer > displayDuration )
            {
                displaying = false;
                transform.DOScale(0f, 0.5f).SetEase(Ease.InBounce);
            }
        }
    }

    public void Display(string text, bool good)
    {
        uiText.text = text;

        Color c = good ? goodColor : badColor;

        image.color = c;
        imageOutline.effectColor = c;
        textOutline.effectColor = c;

        group.SetActive(true);

        transform.localScale = Vector3.zero;

        transform.DOScale(1f, 0.5f).SetEase(Ease.OutBounce);

        displaying = true;
        timer = 0f;

    }
}
