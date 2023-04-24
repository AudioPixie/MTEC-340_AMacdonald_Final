using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicFade : MonoBehaviour
{
    public float startVolume;
    public float fadeTime = 1.5f;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = startVolume;
    }

    public void FadeIn()
    {
        while (audioSource.volume < 0.7f)
            audioSource.volume += Time.deltaTime / fadeTime;
    }

    public void FadeOut()
    {
        while (audioSource.volume > 0f)
            audioSource.volume -= Time.deltaTime / fadeTime;
    }
}
