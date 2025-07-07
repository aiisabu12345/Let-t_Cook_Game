using UnityEngine;

public class UseSFX : MonoBehaviour
{
    AudioManager audioManager;


    private void Awake()
    {
        audioManager = GameObject.FindWithTag("Audio").GetComponent<AudioManager>();
    }

    public void PlayMusic()
    {
        audioManager.PlaySFX(audioManager.btn);
    }
    // audioManager.PlaySFX(audioManager.btn);
}
