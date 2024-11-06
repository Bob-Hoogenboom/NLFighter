using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioComponent : MonoBehaviour
{
    [SerializeField] private AudioData[] audios;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string name)
    {
        AudioData audioData = audios.FirstOrDefault(data => data.name == name);
        if (audioData != null) PlaySound(audioData);
    }

    public void PlaySound(int id)
    {
        if (id >= audios.Length) return;
        PlaySound(audios[id]);
    }

    private void PlaySound(AudioData audio)
    {
        if (audio.clips.Length < 1) return;

        AudioClip currentClip = audio.GetRandomClip();

        //set audio data to source
        audioSource.volume = audio.volume;
        audioSource.pitch = 1f + Random.Range(-audio.pitchShiftRange, audio.pitchShiftRange);
        audioSource.PlayOneShot(currentClip);
    }
}
