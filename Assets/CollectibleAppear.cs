using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleAppear : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        var q = other.GetComponent<QuickTimeEvent>();
        if ( q!= null) {
            q.Appear();
        }
    }
}
