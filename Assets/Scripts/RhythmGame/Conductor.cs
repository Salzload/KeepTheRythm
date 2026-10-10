using UnityEngine;

public class Conductor : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject notePrefab;
    public float[] laneXPos = { -1.5f, -0.5f, 0.5f, 1.5f };
    public float nextBeatToSpawn = 4f;
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

        if(songPosInBeats >= nextBeatToSpawn-2f)
        {
            int lane = Random.Range(0, laneXPos.Length);
            float spawnX = laneXPos[lane];

            Vector3 spawnPos = new Vector3(spawnX, -8f, 0f);
            GameObject newNote = Instantiate(notePrefab, spawnPos, Quaternion.identity);

            Note noteScript = newNote.GetComponent<Note>();
            noteScript.beat = nextBeatToSpawn;
            noteScript.lane = lane;

            nextBeatToSpawn += 1f;
        }
    }
}
