using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTimeEventTrigger : MonoBehaviour
{
    public bool pause = false;

    public GameObject objectToActivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Controller")
        {
            if ( pause)
            {
                VideoManager.Instance.Stop();
                objectToActivate.SetActive(true);
            }
            else
            {
                VideoManager.Instance.Speed_Down();
            }
        }
    }
}
