using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    float timer = 0f;

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Controller") {
            Tween.Bounce(transform);
            timer = 0f;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Controller") {
            Tween.Bounce(transform);
            timer = 0f;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.tag == "Controller") {
            timer += Time.deltaTime;
            if ( timer >= 2f) {
                Tween.Bounce(transform);
                PlayerPrefs.DeleteAll();
                DisplayGlobalScore.Instance.UpdateUI();
                Tween.Bounce(transform);
            }

        }
    }
}
