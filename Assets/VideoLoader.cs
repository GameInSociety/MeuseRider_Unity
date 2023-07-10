using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Android;
using UnityEngine.Video;

public class VideoLoader : MonoBehaviour
{
    public Text uiText;
    public string videoName;

    public VideoPlayer VideoPlayer;
    public string videoUrl;

    // Start is called before the first frame update
    void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageWrite)) {
            Permission.RequestUserPermission(Permission.ExternalStorageWrite);
        }
        if (!Permission.HasUserAuthorizedPermission(Permission.ExternalStorageRead))
        {
            Permission.RequestUserPermission(Permission.ExternalStorageRead);
        }

        videoUrl = Application.persistentDataPath + "/" + videoName + ".mp4";
        uiText.text = videoUrl;
        uiText.text += "data path : " + Application.dataPath;

        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        VideoPlayer.errorReceived += delegate (VideoPlayer videoPlayer, string message)
        {
            uiText.text += "\n" + message;
            Debug.LogWarning("[VideoPlayer] Play Movie Error: " + message);
        };
        VideoPlayer.url = videoUrl;
        VideoPlayer.Prepare();

        while (!VideoPlayer.isPrepared)
        {
            yield return null;
        }

        VideoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
