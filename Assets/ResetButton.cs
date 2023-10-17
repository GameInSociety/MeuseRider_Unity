using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Controller") {
            Tween.Bounce(transform);
            PlayerPrefs.DeleteAll();
            DisplayGlobalScore.Instance.UpdateUI();
        }
    }
}
