using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public enum Type
    {
        FX,
        Ambiant,
        Voice,
    }

    public AudioSource[] sources;

    private void Awake()
    {
        Instance = this;
    }

    public void Play(Type type, AudioClip clip)
    {
        sources[(int)type].clip = clip;
        sources[(int)type].Play();
    }
}
