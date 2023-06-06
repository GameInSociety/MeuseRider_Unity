using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class VR_Button : MonoBehaviour
{
    public Renderer rend;
    public Color emissionColor;

    bool inside = false;



    private void Update()
    {
        if (inside)
        {
            var leftHandDevices = new List<UnityEngine.XR.InputDevice>();
            UnityEngine.XR.InputDevices.GetDevicesAtXRNode(UnityEngine.XR.XRNode.LeftHand, leftHandDevices);

            if (leftHandDevices.Count == 1)
            {
                UnityEngine.XR.InputDevice device = leftHandDevices[0];
                bool triggerValue;
                if (device.TryGetFeatureValue(UnityEngine.XR.CommonUsages.triggerButton, out triggerValue) && triggerValue)
                {
                    Debug.Log("Trigger button is pressed.");
                    SceneManager.LoadScene("Main_Scene");
                }
            }
            else if (leftHandDevices.Count > 1)
            {
                Debug.Log("Found more than one left hand!");
            }
        }

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
