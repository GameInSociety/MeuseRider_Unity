using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using DG.Tweening;

public class Interatable : MonoBehaviour
{
    bool inside = false;

    public float scale_amount = 1.2f;
    public float scale_duration = 0.2f;

    private void Update()
    {
        if (inside)
        {

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Controller" && !inside)
        {
            Enter();        
        }
    }

    void Enter()
    {
        inside = true;

        transform.DOScale(scale_amount, scale_duration).SetEase(Ease.OutBounce);
    }

}
