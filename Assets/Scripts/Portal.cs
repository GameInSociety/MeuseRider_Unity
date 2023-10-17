using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Portal : VR_Button
{
    public static Portal Instance;
    public int levelIndex;
    public TextMeshProUGUI uiText;
    public AudioClip audioClip;

    public override void Update()
    {
        base.Update();
    }

    public void SetText(string text)
    {
        uiText.text = text;
    }

    public override void Trigger()
    {
        base.Trigger();
    }
}
