using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    public static DisplayText Instance;

    public GameObject group;

    public Text uiText;

    float timer = 0f;
    public float displayDuration = 2f;
    bool displaying = false;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (displaying)
        {
            timer += Time.deltaTime;
            if (timer > displayDuration)
            {
                displaying = false;
                transform.DOScale(0f, 0.5f).SetEase(Ease.InBounce);
            }
        }
    }

    public void Display(string text)
    {
        uiText.text = text;

        group.SetActive(true);

        transform.localScale = Vector3.zero;

        transform.DOScale(1f, 0.5f).SetEase(Ease.OutBounce);

        displaying = true;
        timer = 0f;

    }
}
