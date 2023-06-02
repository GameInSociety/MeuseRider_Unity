using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QuickTimeEvent : MonoBehaviour
{
    public Transform body;

    public GameObject trigger;

    public bool booster = false;

    Rigidbody rb;

    public float initScale = 1f;

    bool inBasked = false;

    bool caught = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Controller" && !caught)
        {
            caught= true;

            trigger.gameObject.SetActive(false);
            GetComponent<Animator>().enabled = false;

            if (booster)
            {
                VideoManager.Instance.Speed_Up();
                transform.DOScale(0f, .5f).SetEase(Ease.OutBounce);
            }
            else
            {
                transform.SetParent(other.transform);
                Basket.Instance.ShowFeedback();
                transform.DOScale(initScale, .2f).SetEase(Ease.OutBounce);
                transform.DOLocalMove(new Vector3(-0.0352f, 0.0055f, 0.0349f), 0.2f);
                transform.DOLocalRotate(new Vector3(0, -98f, 70.507f), 0.2f);
            }


        }

        if ( other.tag == "Basket" && !booster)
        {
            if (inBasked)
                return;

            inBasked = true;

            Basket.Instance.HideFeedback();

            DisplayScore.Instance.AddScore();

            rb.useGravity = true;
            rb.isKinematic = false;

            transform.SetParent(other.transform);
            transform.SetParent(null);

            //transform.DOScale(0f, 0.5f).SetEase(Ease.InBounce);

        }
    }


    private void Update()
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
            }
        }
        else if (leftHandDevices.Count > 1)
        {
            Debug.Log("Found more than one left hand!");
        }


        

    }
}
