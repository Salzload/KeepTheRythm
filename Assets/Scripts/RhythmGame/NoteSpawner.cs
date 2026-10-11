using UnityEditor;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] GameObject track;
    [SerializeField] GameObject note;
    [SerializeField] GameObject bpmScanner;
    [SerializeField] Key key;
    [SerializeField] GameObject multiCounter;

    [SerializeField] private SpriteRenderer buttonRenderer;
    [SerializeField] private Sprite neutralSprite;
    [SerializeField] private Sprite clickedSprite;
    [SerializeField] private float feedbackDuration = 0.1f;

    private Coroutine feedbackCoroutine;

    float beat;
    float nullDistance;
    public List<GameObject> notes = new List<GameObject>();
    MultiplikatorCounter counter;

    void Start()
    {
        BpmScanner scanner = bpmScanner.GetComponent<BpmScanner>();
        beat = 60 / scanner.bpm;
        counter = multiCounter.GetComponent<MultiplikatorCounter>();
    }

    void OnDisable()
    {
        ClearAllNotes();
    }

    void Update()
    {
        if (notes.Count == 0 || notes[0] == null) return;

        nullDistance = Vector2.Distance(
            gameObject.transform.position,
            notes[0].transform.position
        );

        if (!Keyboard.current[key].wasPressedThisFrame) return;

        PlayButtonFeedback();

        if (nullDistance > 1f)
        {
            counter.breakMult();
            Debug.Log("Zu weit weg!");
        }
        else if (nullDistance > 0.25f)
        {
            counter.hitNote();
            GameEvents.OnLoveAdded?.Invoke(0.15f * counter.getMult());
            GameObject note = notes[0];
            notes.Remove(note);
            Destroy(note);
        }
        else
        {
            counter.perfectNote();
            GameEvents.OnLoveAdded?.Invoke(0.3f * counter.getMult());
            GameObject note = notes[0];
            notes.Remove(note);
            Destroy(note);
        }
    }

    public void SpawnNote()
    {
        GameObject spawnNote = Instantiate(note);

        NoteBehaviour behaviour = spawnNote.GetComponent<NoteBehaviour>();

        behaviour.Initialized(beat * 4, track, gameObject, multiCounter);

        notes.Add(spawnNote);
    }

    public void ClearAllNotes()
    {
        for (int i = notes.Count - 1; i >= 0; i--)
        {
            if (notes[i] != null)
            {
                Destroy(notes[i]);
            }
        }
        notes.Clear();
    }

    private void PlayButtonFeedback()
    {
        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
        }

        feedbackCoroutine = StartCoroutine(ButtonFeedback());
    }

    private System.Collections.IEnumerator ButtonFeedback()
    {
        buttonRenderer.sprite = clickedSprite;

        yield return new WaitForSeconds(feedbackDuration);

        buttonRenderer.sprite = neutralSprite;
        feedbackCoroutine = null;
    }
}
