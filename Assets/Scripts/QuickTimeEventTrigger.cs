using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickTimeEventTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Controller")
        {
            VideoManager.Instance.Speed_Down();
        }
    }
}
