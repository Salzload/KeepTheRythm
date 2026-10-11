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
    float beat;
    float nullDistance;
    public List<GameObject> notes = new List<GameObject>();

    void Start()
    {
        BpmScanner scanner = bpmScanner.GetComponent<BpmScanner>();
        beat = 60/scanner.bpm;
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
            Debug.Log("Zu weit weg!");
        }
        else if (nullDistance > 0.25f)
        {
            Debug.Log("Guter Hit!");
        }
        else
        {
            Debug.Log("Perfekter Hit!");
        }
    }

    public void SpawnNote()
    {
        GameObject spawnNote = Instantiate(note);

        NoteBehaviour behaviour = spawnNote.GetComponent<NoteBehaviour>();

        behaviour.Initialized(beat*4, track, gameObject);

        notes.Add(spawnNote);
    }
}
