using JetBrains.Annotations;
using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public static VideoManager Instance;

    [SerializeField]
    private RenderTexture _renderTexture = null;

    public Text uiText_Debug;

    public Level[] levels;
    [System.Serializable]
    public class Level
    {
        public string name;
        public GameObject ld_Obj;
    }

    public int levelIndex = 0;

    float currentPlayBackSpeed = 0.5f;
    float currentPathFollowerSpeed = 5;
    public float targetPlayBackSpeed = 0.5f;
    public float targetPathFollowerSpeed = 5f;

    public float defaultPlayBackSpeed = 1f;
    public float defaultFollowerSpeed = 5f;

    public float BOOST_MULT = 1.5f;
    public float SLOW_MULT = 0.5f;

    public float lerpSpeed = 1f;

    float timer = 0f;
    public float speedUpDuration = 2f;

    public Transform endWayPoint;

    bool changeSpeed = false;

    public bool playingVideo = false;

    private void Awake()
    {
        Instance= this;
    }

    private void Update()
    {
        if (playingVideo)
        {
            UpdateVideo();
        }
    }

    void UpdateVideo()
    {
        if (changeSpeed)
        {
            timer += Time.deltaTime;
            if (timer > speedUpDuration)
            {
                Speed_Normal();
                changeSpeed = false;
            }
        }

        currentPlayBackSpeed = Mathf.Lerp(currentPlayBackSpeed, targetPlayBackSpeed, lerpSpeed * Time.deltaTime);
        currentPathFollowerSpeed = Mathf.Lerp(currentPathFollowerSpeed, targetPathFollowerSpeed, lerpSpeed * Time.deltaTime);

        var videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.playbackSpeed = currentPlayBackSpeed;
        PathFollower.Instance.speed = currentPathFollowerSpeed;
    }

    public void Speed_Up()
    {
        targetPathFollowerSpeed = defaultFollowerSpeed * BOOST_MULT;
        targetPlayBackSpeed = defaultPlayBackSpeed * BOOST_MULT;

        DisplayFeedback.Instance.Display("Boost", true);

        changeSpeed = true;
        timer = 0f;
    }

    public void Speed_Normal()
    {
        targetPathFollowerSpeed = defaultFollowerSpeed;
        targetPlayBackSpeed = defaultPlayBackSpeed;
    }

    public void Speed_Down()
    {
        targetPathFollowerSpeed = defaultFollowerSpeed * SLOW_MULT;
        targetPlayBackSpeed = defaultPlayBackSpeed * SLOW_MULT;

        DisplayFeedback.Instance.Display("Miss", false);

        changeSpeed = true;
        timer = 0f;
    }

    public void Stop()
    {
        targetPathFollowerSpeed = 0f;
        targetPlayBackSpeed = 0f;
    }

    private IEnumerator Start()
    {
        TransitionManager.Instance.image.color = Color.black;

        Debug.Log(Application.persistentDataPath);

        Application.runInBackground = true;

        var videoPlayer = GetComponent<VideoPlayer>();
        var audioSource = GetComponent<AudioSource>();

        videoPlayer.errorReceived += delegate (VideoPlayer videoPlayer, string message)
        {
            uiText_Debug.text = message;
            Debug.LogWarning("[VideoPlayer] Play Movie Error: " + message);
        };

        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;

        videoPlayer.playbackSpeed = currentPlayBackSpeed;
        PathFollower.Instance.speed = currentPathFollowerSpeed;

        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        // load level
        foreach (var item in levels)
        {
            if (item.ld_Obj == null)
                continue;

            item.ld_Obj.SetActive(false);
        }

        string url = Application.persistentDataPath + "/" + levels[levelIndex].name + ".mp4";
        videoPlayer.url = url;

        levels[levelIndex].ld_Obj.SetActive(true);
        videoPlayer.Prepare();

        while (!videoPlayer.isPrepared)
            yield return null;

        videoPlayer.Play();
        videoPlayer.Pause();
        videoPlayer.time = 0f;
        videoPlayer.playbackSpeed = 0f;
        videoPlayer.targetTexture = _renderTexture;

        yield return new WaitForSeconds(TransitionManager.Instance.dur);

        playingVideo = true;
        endWayPoint.position = new Vector3((float)videoPlayer.length * 10f, 0f, 0f);
        Debug.Log("video lenght : " + videoPlayer.length);

        PathFollower.Instance.UpdateMovement();

        TransitionManager.Instance.FadeOut();
        yield return new WaitForSeconds(TransitionManager.Instance.dur);

        PathFollower.Instance.StartMovement();

        videoPlayer.Play();
        audioSource.Play();

        while (videoPlayer.isPlaying)
            yield return null;

        Debug.Log("[VIDEO ENDED]");

        playingVideo = false;
        levelIndex++;

        PathFollower.Instance.move = false;


        TransitionManager.Instance.FadeIn();

        yield return new WaitForSeconds(TransitionManager.Instance.dur);

        if (levelIndex == levels.Length)
        {

        }
        else
        {
            StartCoroutine(Start());
        }



    }

}