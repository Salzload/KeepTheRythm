using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DateManager : MonoBehaviour
{
    public static DateManager Instance { get; private set; }

    [SerializeField] private float maxLove = 100f;
    [SerializeField] private float currentLove = 30f;

    public string rhythmSceneName = "Rhythm";
    public string runnerSceneName = "Runner";

    public float questionDuration = 3.5f;
    public float minigameDuration = 15f;

    public List<string> activePowerups = new List<string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        GameEvents.OnLoveAdded += AddLove;
        GameEvents.OnLoveRemoved += RemoveLove;
        GameEvents.OnPowerupCollected += SavePowerup;
    }

    private void OnDisable()
    {
        GameEvents.OnLoveAdded -= AddLove;
        GameEvents.OnLoveRemoved -= RemoveLove;
        GameEvents.OnPowerupCollected -= SavePowerup;
    }

    private void Start()
    {
        UpdateLoveDisplay();
        StartCoroutine(GameLoop());
    }

    public void AddLove(float amount)
    {
        currentLove = Mathf.Clamp(currentLove + amount, 0f, maxLove);
        UpdateLoveDisplay();
    }

    public void RemoveLove(float amount)
    {
        currentLove = Mathf.Clamp(currentLove - amount, 0f, maxLove);
        UpdateLoveDisplay();

        if (currentLove <= 0f)
        {
            StopAllCoroutines();
            UnloadActiveSubScenes();
            GameEvents.OnGameOver?.Invoke();
        }
    }

    public float GetLove() => currentLove;
    public float GetPercentage() => currentLove / maxLove;

    private void SavePowerup(string powerupName)
    {
        activePowerups.Add(powerupName);
    }

    private void UpdateLoveDisplay()
    {
        GameEvents.OnLoveChangedNormalized?.Invoke(GetPercentage());

        float speedMultiplier = Mathf.Lerp(1.0f, 1.8f, GetPercentage());
        GameEvents.OnSpeedMultiplierChanged?.Invoke(speedMultiplier);
    }

    private IEnumerator GameLoop()
    {
        for (int round = 1; round <= 3; round++)
        {
            GameEvents.OnRoundStarted?.Invoke(round);
            yield return new WaitForSeconds(questionDuration);

            yield return SceneManager.LoadSceneAsync(rhythmSceneName, LoadSceneMode.Additive);
            yield return new WaitForSeconds(minigameDuration);

            Scene rhythmScene = SceneManager.GetSceneByName(rhythmSceneName);
            if (rhythmScene.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(rhythmSceneName);
            }

            if (currentLove >= maxLove)
            {
                GameEvents.OnVictory?.Invoke();
                yield break;
            }

            yield return new WaitForSeconds(1f);

            yield return SceneManager.LoadSceneAsync(runnerSceneName, LoadSceneMode.Additive);
            yield return new WaitForSeconds(minigameDuration);

            Scene runnerScene = SceneManager.GetSceneByName(runnerSceneName);
            if (runnerScene.isLoaded)
            {
                yield return SceneManager.UnloadSceneAsync(runnerSceneName);
            }

            if (currentLove >= maxLove)
            {
                GameEvents.OnVictory?.Invoke();
                yield break;
            }

            yield return new WaitForSeconds(1f);
        }

        if (GetPercentage() >= 0.7f)
        {
            GameEvents.OnVictory?.Invoke();
        }
        else
        {
            GameEvents.OnGameOver?.Invoke();
        }
    }

    private void UnloadActiveSubScenes()
    {
        Scene rhythm = SceneManager.GetSceneByName(rhythmSceneName);
        if (rhythm.isLoaded)
        {
            SceneManager.UnloadSceneAsync(rhythmSceneName);
        }

        Scene runner = SceneManager.GetSceneByName(runnerSceneName);
        if (runner.isLoaded)
        {
            SceneManager.UnloadSceneAsync(runnerSceneName);
        }
    }
}