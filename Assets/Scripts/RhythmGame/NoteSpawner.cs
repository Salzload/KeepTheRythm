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
    float beat;
    float nullDistance;
    public List<GameObject> notes = new List<GameObject>();
    MultiplikatorCounter counter;

    void Start()
    {
        BpmScanner scanner = bpmScanner.GetComponent<BpmScanner>();
        beat = 60/scanner.bpm;
        counter = multiCounter.GetComponent<MultiplikatorCounter>();
    }

    void Update()
    {
        if (notes.Count == 0 || notes[0] == null) return;

        nullDistance = Vector2.Distance(
            gameObject.transform.position,
            notes[0].transform.position
        );

        if (!Keyboard.current[key].wasPressedThisFrame) return;

        if (nullDistance > 1f)
        {
            counter.breakMult();
            Debug.Log("Zu weit weg!");
        }
        else if (nullDistance > 0.25f)
        {
            counter.hitNote();
            GameObject note = notes[0];
            notes.Remove(note);
            Destroy(note);
            Debug.Log("Guter Hit! Aktueller Multplikator:" + counter.getMult());
        }
        else
        {
            counter.perfectNote();
            GameObject note = notes[0];
            notes.Remove(note);
            Destroy(note);
            Debug.Log("Perfekter Hit!  Aktueller Multplikator:" + counter.getMult());
        }
    }

    public void SpawnNote()
    {
        GameObject spawnNote = Instantiate(note);

        NoteBehaviour behaviour = spawnNote.GetComponent<NoteBehaviour>();

        behaviour.Initialized(beat*4, track, gameObject, multiCounter);

        notes.Add(spawnNote);
    }
}
