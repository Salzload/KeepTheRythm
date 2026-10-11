using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameFlowController : MonoBehaviour
{
    [SerializeField] private PanelRenderer panelRenderer;
    [SerializeField] private Animator directorAnimator;
    [SerializeField] private GameObject[] gameplayElements;

    private VisualElement root;
    private Button playButton;

    void OnEnable()
    {
        if (panelRenderer != null)
        {
            panelRenderer.RegisterUIReloadCallback(OnUIReload);
        }
    }

    void OnDisable()
    {
        if (panelRenderer != null)
        {
            panelRenderer.UnregisterUIReloadCallback(OnUIReload);
        }
    }

    void Start()
    {
        SetGameplayElementsActive(false);
    }

    private void OnUIReload(PanelRenderer renderer, VisualElement rootElement, int version)
    {
        root = rootElement;
        playButton = root.Q<Button>("Play");

        if (playButton != null)
        {
            playButton.clicked += OnPlayPressed;
        }

    }


    private void OnPlayPressed()
    {
        StartCoroutine(StartGameSequence());
    }

    private IEnumerator StartGameSequence()
    {
        root.style.opacity = 0f;

        directorAnimator.SetTrigger("StartPlay");

        yield return new WaitForSeconds(2.0f);

        SetGameplayElementsActive(true);
    }

    private void SetGameplayElementsActive(bool state)
    {
        foreach (GameObject go in gameplayElements)
        {
            if (go != null) go.SetActive(state);
        }
    }
}