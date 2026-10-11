using UnityEngine;
using System.Collections.Generic;

public class Conductor : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject notePrefab;
    public float[] laneXPos = { -2f, -0.66f, 0.66f, 2f };
    public float nextBeatToSpawn = 4f;
    public int nextNoteIndex = 0;
    public float bpm = 120f;
    public float secPerBeat;
    public float songPos;
    public float songPosInBeats;
    public double dspSongStartTime;

    public float spawnY = -8f;
    public float targetY = 3f;

    public List<Note>[] notesInLanes = new List<Note>[4];
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        secPerBeat = 60f / bpm;
        dspSongStartTime = AudioSettings.dspTime + 1.0;
        audioSource.PlayScheduled(dspSongStartTime);

        for (int i = 0; i < notesInLanes.Length; i++)
        {
            notesInLanes[i] = new List<Note>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        songPos = (float)(AudioSettings.dspTime - dspSongStartTime);
        songPosInBeats = songPos / secPerBeat;

        if(songPosInBeats >= nextBeatToSpawn-2f)
        {
            int lane = Random.Range(0, laneXPos.Length);
            float spawnX = laneXPos[lane];

            Vector3 spawnPos = new Vector3(spawnX, targetY, 0f);
            GameObject newNote = Instantiate(notePrefab, spawnPos, Quaternion.identity);

            Note noteScript = newNote.GetComponent<Note>();
            noteScript.beat = nextBeatToSpawn;
            noteScript.lane = lane;
            noteScript.startY = spawnY;
            noteScript.targetY = targetY;
            notesInLanes[lane].Add(noteScript);

            nextBeatToSpawn += 1f;
        }
    }

    public void HitNote(int laneIndex)
    {
        while (notesInLanes[laneIndex].Count > 0 &&
               songPosInBeats > notesInLanes[laneIndex][0].beat + 0.25f)
        {
            notesInLanes[laneIndex].RemoveAt(0);
        }

        if (notesInLanes[laneIndex].Count == 0) return;

        Note targetNote = notesInLanes[laneIndex][0];
        float beatDiff = Mathf.Abs(songPosInBeats - targetNote.beat);

        if (beatDiff <= 0.25f)
        {
            Debug.Log($"Hit auf Lane {laneIndex}! Diff: {beatDiff}");
            notesInLanes[laneIndex].RemoveAt(0);
            Destroy(targetNote.gameObject);
        }
        else
        {
            Debug.Log("Zu früh!");
        }
    }
}
