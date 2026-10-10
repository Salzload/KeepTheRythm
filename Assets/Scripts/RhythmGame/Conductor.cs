using UnityEngine;

public class Conductor : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject notePrefab;
    public float[] laneXPos = { -1.5f, -0.5f, 0.5f, 1.5f };
    public float[] noteBeats = { 4f, 5f, 6f, 7f, 8f, 9f, 10f };
    public int[] noteLanes = { 0, 1, 2, 3, 2, 1, 0 };
    public int nextNoteIndex = 0;
    public float bpm = 120f;
    public float secPerBeat;
    public float songPos;
    public float songPosInBeats;
    public double dspSongStartTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        secPerBeat = 60f / bpm;
        dspSongStartTime = AudioSettings.dspTime + 1.0;
        audioSource.PlayScheduled(dspSongStartTime);
    }

    // Update is called once per frame
    void Update()
    {
        songPos = (float)(AudioSettings.dspTime - dspSongStartTime);
        songPosInBeats = songPos / secPerBeat;

        if(nextNoteIndex < noteBeats.Length && songPosInBeats >= noteBeats[nextNoteIndex]-2f)
        {
            int lane = noteLanes[nextNoteIndex];
            float spawnX = laneXPos[lane];

            Vector3 spawnPos = new Vector3(spawnX, -8f, 0f);
            GameObject newNote = Instantiate(notePrefab, spawnPos, Quaternion.identity);
            Note noteScript = newNote.GetComponent<Note>();
            noteScript.beat = noteBeats[nextNoteIndex];
            noteScript.lane = lane;

            nextNoteIndex++;
        }
    }
}
