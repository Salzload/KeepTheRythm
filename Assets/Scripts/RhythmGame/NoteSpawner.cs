using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    [SerializeField] GameObject track;
    [SerializeField] GameObject note;
    [SerializeField] float bpm = 130f;
    float beat;
    float trackTime = 0;


    void Start()
    {
        gameObject.GetComponent<AudioSource>().Play();
        beat = 60/bpm;
        trackTime += Time.deltaTime;
    }

    void Update()
    {
        
    }
}
