using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Der Lautsprecher")]
    public AudioSource sfxSource; // Ziehe hier deine Audio Source Komponente rein!

    [Header("Sound-Dateien (AudioClips)")]
    public AudioClip roundStartClip;   // Sound zu Beginn einer Runde
    public AudioClip loveAddedClip;    // Sound bei Treffer / Pluspunkten
    public AudioClip loveRemovedClip;  // Sound bei Schaden / Red Flag
    public AudioClip powerupClip;      // Sound bei Powerup
    public AudioClip victoryClip;      // Sound bei Date gewonnen
    public AudioClip gameOverClip;     // Sound bei Date verloren

    private void OnEnable()
    {
        // Hört auf alle wichtigen Ereignisse im Spiel!
        GameEvents.OnRoundStarted += HandleRoundStarted;
        GameEvents.OnLoveAdded += HandleLoveAdded;
        GameEvents.OnLoveRemoved += HandleLoveRemoved;
        GameEvents.OnPowerupCollected += HandlePowerup;
        GameEvents.OnVictory += HandleVictory;
        GameEvents.OnGameOver += HandleGameOver;
    }

    private void OnDisable()
    {
        GameEvents.OnRoundStarted -= HandleRoundStarted;
        GameEvents.OnLoveAdded -= HandleLoveAdded;
        GameEvents.OnLoveRemoved -= HandleLoveRemoved;
        GameEvents.OnPowerupCollected -= HandlePowerup;
        GameEvents.OnVictory -= HandleVictory;
        GameEvents.OnGameOver -= HandleGameOver;
    }

    // --- DIE REAKTIONEN ---
    private void HandleRoundStarted(int round)
    {
        PlaySound(roundStartClip);
    }

    private void HandleLoveAdded(float amount)
    {
        PlaySound(loveAddedClip);
    }

    private void HandleLoveRemoved(float amount)
    {
        PlaySound(loveRemovedClip);
    }

    private void HandlePowerup(string powerupName)
    {
        PlaySound(powerupClip);
    }

    private void HandleVictory()
    {
        PlaySound(victoryClip);
    }

    private void HandleGameOver()
    {
        PlaySound(gameOverClip);
    }

    private void PlaySound(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
}