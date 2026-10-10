using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DateUI : MonoBehaviour
{
    public Slider loveSlider;
    public TextMeshProUGUI percentageText;
    public TextMeshProUGUI dialogueText;
    public GameObject winScreen;
    public GameObject gameOverScreen;

    private void OnEnable()
    {
        GameEvents.OnLoveChangedNormalized += UpdateBar;
        GameEvents.OnRoundStarted += ShowQuestion;
        GameEvents.OnGameOver += ShowGameOver;
        GameEvents.OnVictory += ShowVictory;
    }

    private void OnDisable()
    {
        GameEvents.OnLoveChangedNormalized -= UpdateBar;
        GameEvents.OnRoundStarted -= ShowQuestion;
        GameEvents.OnGameOver -= ShowGameOver;
        GameEvents.OnVictory -= ShowVictory;
    }

    private void UpdateBar(float normalized)
    {
        if (loveSlider) loveSlider.value = normalized;
        if (percentageText) percentageText.text = $"{Mathf.RoundToInt(normalized * 100)}%";
    }

    private void ShowQuestion(int round)
    {
        if (dialogueText) dialogueText.text = $"Runde {round}: Erzähl mir was!";
    }

    private void ShowGameOver() => gameOverScreen?.SetActive(true);
    private void ShowVictory() => winScreen?.SetActive(true);
}