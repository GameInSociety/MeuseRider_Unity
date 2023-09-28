using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartButton : MonoBehaviour
{
    public static StartButton Instance;

    public Transform scoreTransform;
    public Transform body;
    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Controller")
        {
            VideoManager.Instance.StartVideos();

            transform.DOScale(0f, 0.5f).SetEase(Ease.InBounce);
            scoreTransform.DOScale(0f, 0.5f).SetEase(Ease.InBounce);

        }
    }

    public void ResetButton()
    {
        transform.localScale = Vector3.one;
        scoreTransform.localScale = Vector3.one; 
    }
}
