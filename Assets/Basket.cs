using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    public static Basket Instance;

    public GameObject feedback_obj;

    public GameObject goodFeedback_Obj;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowFeedback()
    {
        feedback_obj.SetActive(true);
    }

    public void HideFeedback()
    {
        feedback_obj.SetActive(false);

        goodFeedback_Obj.SetActive(true);

        CancelInvoke("delay");
        Invoke("delay", 1f);
    }

    void delay()
    {
        goodFeedback_Obj.SetActive(false);
    }
}
