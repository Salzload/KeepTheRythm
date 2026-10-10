using UnityEngine;

public class RedFlagSpawner : MonoBehaviour
{
    [SerializeField] private GameObject redFlag;
    [SerializeField] private GameObject[] tracks;
    [SerializeField] private float totalTime;

    void Start()
    {
        
    }

    void Update()
    {
        totalTime += Time.deltaTime;
        if (totalTime >= 1)
        {
            totalTime -= 1;
            HandleSpawn();
        }
    }

    private void HandleSpawn()
    {
        GameObject spawnTrack = tracks[Random.Range(0, tracks.Length)];
        GameObject flag = Instantiate(redFlag);

        RedFlagBehaviour behaviour = flag.GetComponent<RedFlagBehaviour>();

        behaviour.Initialized(spawnTrack, redFlag);
    }
}
