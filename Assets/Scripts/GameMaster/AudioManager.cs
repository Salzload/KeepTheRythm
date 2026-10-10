using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioClip hitClip;
    public AudioClip missClip;
    public AudioClip powerupClip;

    private void OnEnable()
    {
        GameEvents.OnLoveAdded += (amount) => PlaySFX(hitClip);
        GameEvents.OnLoveRemoved += (amount) => PlaySFX(missClip);
        GameEvents.OnPowerupCollected += (type) => PlaySFX(powerupClip);
    }

    private void OnDisable()
    {
        // Unsubscribe analog zu OnEnable
    }

    private void PlaySFX(AudioClip clip)
    {
        if (sfxSource && clip) sfxSource.PlayOneShot(clip);
    }
}