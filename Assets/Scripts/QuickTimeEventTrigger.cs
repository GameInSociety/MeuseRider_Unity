using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTimeEventTrigger : MonoBehaviour
{
    public bool pause = false;
    public bool pauseOnPause = false;

    public GameObject objectToActivate;

    public AudioClip audioClip;
    public string text;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Controller")
        {
            if ( pause)
            {
                if ( pauseOnPause)
                {
                    //VideoManager.Instance.Stop();
                }

                if (audioClip != null)
                { 
                    SoundManager.Instance.Play(SoundManager.Type.Voice, audioClip);
                    gameObject.SetActive(false);
                }

                if ( !string.IsNullOrEmpty(text) )
                {
                    DisplayText.Instance.Display(text);
                }

                objectToActivate.SetActive(true);
            }
            else
            {
                //VideoManager.Instance.Speed_Down();
            }
        }
    }
}
