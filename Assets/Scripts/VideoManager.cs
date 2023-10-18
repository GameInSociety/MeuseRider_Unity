using JetBrains.Annotations;
using PathCreation.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
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
        public AudioClip ambiance_clip;
        public AudioClip voice_clip;
    }

    public bool debug_enablePortals;

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


    float boost_Timer = 0f;
    public float video_timer = 0f;
    
    public float speedUpDuration = 2f;

    public Animator bikeAnimator;

    public Transform endWayPoint;

    bool changeSpeed = false;

    public bool playingVideo = false;

    private void Awake()
    {
        Instance= this;

        QualitySettings.vSyncCount = 0;  // VSync must be disabled
    }

    private void Start()
    {
        //LoadVideo();

        bikeAnimator.enabled = false;
    }

    public void StartVideos()
    {
        LoadVideo();
        startGroup.SetActive(false);
        bikeAnimator.enabled = true;
    }

    public void LoadVideo()
    {
        StartCoroutine(LoadVideoCoroutine());
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
            boost_Timer += Time.deltaTime;
            if (boost_Timer > speedUpDuration)
            {
                Speed_Normal();
                changeSpeed = false;
            }
        }

        video_timer += Time.deltaTime;

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
        boost_Timer = 0f;
    }

    public void Speed_Normal()
    {
        bikeAnimator.enabled = true;

        targetPathFollowerSpeed = defaultFollowerSpeed;
        targetPlayBackSpeed = defaultPlayBackSpeed;
    }

    public void Speed_Down()
    {
        targetPathFollowerSpeed = defaultFollowerSpeed * SLOW_MULT;
        targetPlayBackSpeed = defaultPlayBackSpeed * SLOW_MULT;

        DisplayFeedback.Instance.Display("Miss", false);

        changeSpeed = true;
        boost_Timer = 0f;
    }

    public void Stop()
    {
        currentPathFollowerSpeed = 0f;
        currentPlayBackSpeed = 0f;

        targetPathFollowerSpeed = 0f;
        targetPlayBackSpeed = 0f;

        bikeAnimator.enabled = false;
    }

    private IEnumerator LoadVideoCoroutine()
    {
        TransitionManager.Instance.image.color = Color.black;

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

        SoundManager.Instance.Play(SoundManager.Type.Ambiant, levels[levelIndex].ambiance_clip);

        videoPlayer.Play();
        audioSource.Play();
        videoPlayer.time = 0f;
        videoPlayer.targetTexture = _renderTexture;
        playingVideo = true;
        video_timer = 0f;
        Speed_Normal();
        PathFollower.Instance.StartMovement();

        float dur = (float)videoPlayer.length;
        endWayPoint.position = new Vector3(dur * defaultFollowerSpeed, 0f, 0f);

        PathFollower.Instance.UpdateMovement();
        TransitionManager.Instance.FadeOut();

        yield return new WaitForSeconds(2f);

        SoundManager.Instance.Play(SoundManager.Type.Voice, levels[levelIndex].voice_clip);

        yield return new WaitForSeconds((float)videoPlayer.length-4f);

        Debug.Log("[VIDEO ENDED]");

        NextVideo();
    }

    public void NextVideo()
    {
        StartCoroutine(NextVideoCoroutine());
    }

    public GameObject startGroup;

    IEnumerator NextVideoCoroutine()
    {
        Debug.Log("next video");

        levelIndex++;

        TransitionManager.Instance.FadeIn();

        yield return new WaitForSeconds(TransitionManager.Instance.dur);

        if (levelIndex == levels.Length) {
            levelIndex = 0;
            PathFollower.Instance.ResetPos();
            //startGroup.SetActive(true);
            bikeAnimator.enabled = false;
            Stop();
            DisplayGlobalScore.Instance.score.Save();

            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene(0);

        }
        else
        {
            LoadVideo();
        }

    }

}