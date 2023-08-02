using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class VR_Button : MonoBehaviour
{
    public Renderer rend;
    public Color emissionColor;

    public float scaleAmount = 1.2f;
    public float scaleDur = 0.2f;

    bool inside = false;

    public virtual void Update()
    {
        if (inside)
        {
            if (InteractionManager.Instance.PressAny())
            {
                Trigger();
                //SceneManager.LoadScene("Main_Scene");
            }
        }

    }

    public virtual void Trigger()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Controller")
        {
            Enter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if ( other.tag == "Controller")
        {
            Exit();
        }
    }

    void Enter()
    {
        inside = true;
        transform.DOKill();
        transform.DOScale(1.2f, 0.3f).SetEase(Ease.OutBounce);
        rend.material.DOColor(emissionColor, "_EmissionColor", 0.2f);
    }

    void Exit()
    {
        inside = false;
        transform.DOKill();
        transform.DOScale(1f, 0.2f).SetEase(Ease.InBounce);
        rend.material.DOColor(Color.black, "_EmissionColor", 0.2f);
    }
}
