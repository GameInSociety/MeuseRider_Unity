using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public static VideoManager Instance;

    [SerializeField]
    private RenderTexture _renderTexture = null;

    [SerializeField]
    private VideoClip[] _videos = null;
    public int videoIndex = 0;

    float currentPlayBackSpeed = 0.5f;
    float currentPathFollowerSpeed = 5;
    public float targetPlayBackSpeed = 0.5f;
    public float targetPathFollowerSpeed = 5;

    public float lerpSpeed = 1f;

    float timer = 0f;
    public float speedUpDuration = 2f;

    public Transform endWayPoint;


    bool changeSpeed = false;

    private void Awake()
    {
        Instance= this;
    }

    private void Update()
    {
        if ( changeSpeed)
        {
            timer += Time.deltaTime;
            if ( timer > speedUpDuration)
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
        targetPathFollowerSpeed = 15f;
        targetPlayBackSpeed = 1.5f;

        DisplayFeedback.Instance.Display("Boost", true);

        changeSpeed = true;
        timer = 0f;
    }

    public void Speed_Normal()
    {
        targetPathFollowerSpeed = 5;
        targetPlayBackSpeed = 0.5f;
    }

    public void Speed_Down()
    {
        targetPathFollowerSpeed = 2f;
        targetPlayBackSpeed = 0.2f;

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
        Application.runInBackground = true;

        var videoPlayer = GetComponent<VideoPlayer>();
        var audioSource = GetComponent<AudioSource>();


        videoPlayer.errorReceived += delegate (VideoPlayer videoPlayer, string message)
        {
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
        videoPlayer.clip = _videos[videoIndex];
        videoPlayer.Prepare();

        Debug.Log("video lenght : " +videoPlayer.clip.length);

        endWayPoint.position = new Vector3( (float)videoPlayer.clip.length * 10f , 0f , 0f );

        while (!videoPlayer.isPrepared)
            yield return null;

        PathFollower.Instance.enabled = true;
        videoPlayer.targetTexture = _renderTexture;
        videoPlayer.Play();

        audioSource.Play();

        while (videoPlayer.isPlaying)
            yield return null;

        Debug.Log("video ended");

        videoIndex++;

        PathFollower.Instance.enabled = false;

        if ( videoIndex == _videos.Length)
        {

        }
        else
        {
            StartCoroutine(Start());
        }

    }

}