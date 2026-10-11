using UnityEngine;

public class BpmScanner : MonoBehaviour
{

    [SerializeField] private GameObject[] buttons;
    [SerializeField] public float bpm = 130f;
    float beat;
    float trackTime = 0;
    float spawnTime;

    void Start()
    {
        gameObject.GetComponent<AudioSource>().Play();
        beat = 60/bpm;
        spawnTime = beat;
    }

    void Update()
    {
        trackTime += Time.deltaTime;
        if(trackTime >= spawnTime)
        {
            buttons[Random.Range(0, buttons.Length)].GetComponent<NoteSpawner>().SpawnNote();
            spawnTime += beat;
        }
    }
}
