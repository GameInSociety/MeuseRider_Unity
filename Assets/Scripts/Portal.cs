using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;

public class Portal : VR_Button
{
    public static Portal Instance;
    
    public int levelIndex;

    public TextMeshProUGUI uiText;

    public AudioClip audioClip;

    private void Awake()
    {
        Instance = this;
    }

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

        SoundManager.Instance.Play(SoundManager.Type.FX, audioClip);

        gameObject.SetActive(false);
        VideoManager.Instance.NextVideo();

        Debug.Log("triggering thing");
    }
}
