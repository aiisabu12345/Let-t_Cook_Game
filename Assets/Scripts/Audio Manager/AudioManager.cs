using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("---AUDIO Source---")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---AUDIO Clip---")]
    public AudioClip background;
    public AudioClip btn;
    public AudioClip correct;
    public AudioClip winner;
    public AudioClip walk;
    public AudioClip pick;

   
    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void ResetMusic()
    {
        musicSource.Stop();
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
