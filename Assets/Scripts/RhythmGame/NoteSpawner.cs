using UnityEditor;
using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] GameObject track;
    [SerializeField] GameObject note;
    [SerializeField] GameObject bpmScanner;
    float beat;
    List<GameObject> notes = new List<GameObject>();

    void Start()
    {
        BpmScanner scanner = bpmScanner.GetComponent<BpmScanner>();
        beat = 60/scanner.bpm;
    }

    void Update()
    {
        
    }

    public void SpawnNote()
    {
        GameObject spawnNote = Instantiate(note);

        NoteBehaviour behaviour = spawnNote.GetComponent<NoteBehaviour>();

        behaviour.Initialized(beat*4, track);
    }
}
