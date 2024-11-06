using System;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class AudioData
{
    public string name;
    public AudioClip[] clips;
    public AudioMixerGroup audioGroup;
    [Range(0.0f, 1.0f)] public float volume = 1f;
    [Range(0.0f, 3.0f)] public float pitchShiftRange = 0.1f;

    public AudioClip GetRandomClip() => clips[UnityEngine.Random.Range(0, clips.Length)];
}
