using System.Collections;
using TMPro;
using UnityEngine;

public class ComboUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI comboText;
    [SerializeField] private RectTransform rectTransform;

    [SerializeField] private Gradient comboGradient;
    [SerializeField] private int maxComboForColor = 35;

    [SerializeField] private float punchScale = 1.35f;
    [SerializeField] private float punchDuration = 0.15f;
    [SerializeField] private float baseShakeIntensity = 6f;

    private Vector2 originalPosition;
    private Coroutine currentEffect;

    void Awake()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        originalPosition = rectTransform.anchoredPosition;
    }

    void OnEnable()
    {
        GameEvents.OnComboChanged += UpdateComboUI;
    }

    void OnDisable()
    {
        GameEvents.OnComboChanged -= UpdateComboUI;
    }

    void Start()
    {
        comboText.text = "";
    }

    private void UpdateComboUI(int combo, int mult)
    {
        if (combo < 2)
        {
            comboText.text = "";
            rectTransform.anchoredPosition = originalPosition;
            rectTransform.localScale = Vector3.one;
            return;
        }

        comboText.text = combo + "x Combo! (x" + mult + ")";

        float progress = Mathf.Clamp01((float)combo / maxComboForColor);
        comboText.color = comboGradient.Evaluate(progress);

        if (currentEffect != null)
        {
            StopCoroutine(currentEffect);
        }

        currentEffect = StartCoroutine(AnimateHit(combo));
    }

    private IEnumerator AnimateHit(int combo)
    {
        float elapsed = 0f;

        float comboGrowth = Mathf.Clamp(1f + (combo * 0.01f), 1f, 1.4f);
        Vector3 startScale = Vector3.one * (comboGrowth * punchScale);
        Vector3 endScale = Vector3.one * comboGrowth;

        float currentShake = Mathf.Clamp(baseShakeIntensity + (combo * 0.3f), baseShakeIntensity, baseShakeIntensity * 3f);

        while (elapsed < punchDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / punchDuration;

            rectTransform.localScale = Vector3.Lerp(startScale, endScale, t);

            Vector2 randomOffset = Random.insideUnitCircle * currentShake * (1f - t);
            rectTransform.anchoredPosition = originalPosition + randomOffset;

            yield return null;
        }

        rectTransform.localScale = endScale;
        rectTransform.anchoredPosition = originalPosition;
        currentEffect = null;
    }
}