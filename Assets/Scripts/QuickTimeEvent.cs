using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class QuickTimeEvent : MonoBehaviour
{
    public Transform body;

    public GameObject trigger;

    public bool pause = false;
    public bool booster = false;
    public bool startCollectible = false;

    public AudioClip boost_clip;
    public AudioClip score_clip;
    
    public float initScale = 1f;
    public int type;

    bool inBasked = false;

    bool caught = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Controller" && !caught)
        {
            caught= true;

            trigger.gameObject.SetActive(false);
            GetComponent<Animator>().enabled = false;

            if (pause)
            {
                VideoManager.Instance.Speed_Normal();
                transform.DOScale(0f, .5f).SetEase(Ease.OutBounce);
                SoundManager.Instance.Play(SoundManager.Type.FX, boost_clip);
            }
            else if (booster)
            {
                VideoManager.Instance.Speed_Up();
                transform.DOScale(0f, .5f).SetEase(Ease.OutBounce);
                DisplayGlobalScore.Instance.score.Add(4);

                SoundManager.Instance.Play(SoundManager.Type.FX, boost_clip);
            }
            else
            {
                transform.SetParent(other.transform);
                Basket.Instance.ShowFeedback();
                transform.DOScale(initScale, .2f).SetEase(Ease.OutBounce);
                transform.DOLocalMove(new Vector3(-0.0352f, 0.0055f, 0.0349f), 0.2f);
                transform.DOLocalRotate(new Vector3(0, -98f, 70.507f), 0.2f);

                SoundManager.Instance.Play(SoundManager.Type.FX, score_clip);
            }

        }

        if ( other.tag == "Basket" && !booster && caught)
        {
            if (inBasked)
                return;

            inBasked = true;

            Basket.Instance.HideFeedback();

            GetComponent<BoxCollider>().isTrigger = false;

            if (startCollectible)
            {
                VideoManager.Instance.StartVideos();
            }
            else
            {
                Renderer rend = GetComponentInChildren<Renderer>(true);
                DisplayGlobalScore.Instance.score.Add(type);
                DisplayScore.Instance.Display();
            }
            
            transform.SetParent(other.transform);

            Vector3 p = transform.position;
            p.y = 0.55f;
            transform.DOMove(p, 0.2f);
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
