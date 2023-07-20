using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;
    public Image image;
    public float dur = 2f;

    private void Awake()
    {
        Instance = this;
    }

    public void FadeIn()
    {
        image.color = Color.clear;
        image.DOFade(1f, dur);
    }

    public void FadeOut()
    {
        image.DOFade(0f, dur);
    }

}
