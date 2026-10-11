using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameTest : MonoBehaviour
{

        void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            RestartGame();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Wichtig falls Game Over die Zeit gestoppt hat
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

        /*if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("<color=green>TEST: Treffer simuliert (+5)</color>");
            GameEvents.OnLoveAdded?.Invoke(5f);
        }

        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Debug.Log("<color=red>TEST: Miss/Crash simuliert (-10)</color>");
            GameEvents.OnLoveRemoved?.Invoke(10f);
        }

        if (Keyboard.current.wKey.wasPressedThisFrame)
        {
            Debug.Log("<color=yellow>TEST: Powerup 'Shield' gesammelt</color>");
            GameEvents.OnPowerupCollected?.Invoke("Shield");
        }

        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            Debug.Log("<color=black>TEST: Instant Game Over</color>");
            GameEvents.OnLoveRemoved?.Invoke(100f);
        } */
}