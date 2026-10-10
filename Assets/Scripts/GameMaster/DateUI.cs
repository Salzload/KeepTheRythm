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

    [Header("Shake Duration")]
    public float damageShakeDuration = 0.2f;
    public float damageShakeStrength = 5f;

    private Coroutine activeShakeCoroutine;
    private Vector2 shakeBasePosition;
    private bool isShaking = false;

    private void HandleDamageShake(float amount)
    {
        if (activeShakeCoroutine != null)
        {
            StopCoroutine(activeShakeCoroutine);
        }

        activeShakeCoroutine = StartCoroutine(ShakeGBARoutine());
    }

    private IEnumerator ShakeGBARoutine()
    {

        if (!isShaking)
        {
            shakeBasePosition = gbaContainer.anchoredPosition;
            isShaking = true;
        }

        float elapsed = 0f;

        while (elapsed < damageShakeDuration)
        {
            float offsetX = Random.Range(-damageShakeStrength, damageShakeStrength);
            float offsetY = Random.Range(-damageShakeStrength, damageShakeStrength);

            gbaContainer.anchoredPosition = shakeBasePosition + new Vector2(offsetX, offsetY);

            elapsed += Time.deltaTime;
            yield return null;
        }

        gbaContainer.anchoredPosition = shakeBasePosition;
        isShaking = false;
        activeShakeCoroutine = null;
    }

    private void HandleLovePop(float amount)
    {
        StartCoroutine(PopBarRoutine());
    }

    private IEnumerator PopBarRoutine()
    {
        if (loveSlider == null) yield break;

        loveSlider.transform.localScale = Vector3.one * 1.15f;
        yield return new WaitForSeconds(0.08f);
        loveSlider.transform.localScale = Vector3.one;
    }

    private void OnEnable()
    {
        GameEvents.OnLoveChangedNormalized += UpdateBar;
        GameEvents.OnRoundStarted += ShowQuestion;
        GameEvents.OnGameOver += ShowGameOver;
        GameEvents.OnVictory += ShowVictory;

        GameEvents.OnLoveRemoved += HandleDamageShake;
        GameEvents.OnLoveAdded += HandleLovePop;

    }

    private void OnDisable()
    {
        GameEvents.OnLoveChangedNormalized -= UpdateBar;
        GameEvents.OnRoundStarted -= ShowQuestion;
        GameEvents.OnGameOver -= ShowGameOver;
        GameEvents.OnVictory -= ShowVictory;

        GameEvents.OnLoveRemoved -= HandleDamageShake;
        GameEvents.OnLoveAdded -= HandleLovePop;
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