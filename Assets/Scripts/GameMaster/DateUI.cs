using System.Collections;
using System.Collections.Generic;
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

    [Header("Game Boy Animation")]
    public RectTransform gbaContainer;
    public RawImage gbaScreenRawImage;

    public Vector2 gbaPlayingPos = new Vector2(0, 0);
    public Vector2 gbaTalkingPos = new Vector2(0, -120f);
    public float animationSpeed = 8f;

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

    public void LowerGameboy()
    {
        if (gbaScreenRawImage != null)
        {
            gbaScreenRawImage.color = Color.black;
        }

        StopAllCoroutines();
        StartCoroutine(SlideGBARoutine(gbaTalkingPos));
    }

    public void RaiseGameBoy()
    {
        if (gbaScreenRawImage != null)
        {
            gbaScreenRawImage.color = Color.white;
        }

        StopAllCoroutines();
        StartCoroutine(SlideGBARoutine(gbaPlayingPos));
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

    private IEnumerator SlideGBARoutine(Vector2 targetPos)
    {
        while (Vector2.Distance(gbaContainer.anchoredPosition, targetPos) > 0.5f)
        {
            gbaContainer.anchoredPosition = Vector2.Lerp(
                gbaContainer.anchoredPosition,
                targetPos,
                Time.deltaTime * animationSpeed
            );
            yield return null;
        }
        gbaContainer.anchoredPosition = targetPos;
    }
}