using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TurnClockUI : MonoBehaviour
{
    [Header("UI")]
    public Image clockImage;
    public Sprite[] roundSprites;

    [Header("shakies")]
    public float shakeInterval = 3f;
    public float shakeDuration = 0.4f;
    public float shakeAmount = 8f;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        GameEvents.OnRoundStarted += SetClockState;
        StartCoroutine(JiggleRoutine());
    }

    private void OnDisable()
    {
        GameEvents.OnRoundStarted -= SetClockState;
        StopAllCoroutines();
    }


    private void SetClockState(int round)
    {
        int index = round - 1;
        if (roundSprites != null && index >= 0 && index < roundSprites.Length)
        {
            clockImage.sprite = roundSprites[index];
        }
    }

    private IEnumerator JiggleRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(shakeInterval);

            float elapsed = 0f;
            while (elapsed < shakeDuration)
            {
                float zAngle = Mathf.Sin(elapsed * 35f) * shakeAmount;
                rectTransform.localRotation = Quaternion.Euler(0, 0, zAngle);

                elapsed += Time.deltaTime;
                yield return null;
            }

            rectTransform.localRotation = Quaternion.identity;
        }
    }
}