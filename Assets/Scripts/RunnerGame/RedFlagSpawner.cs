using Unity.Collections;
using UnityEngine;
using System.Collections.Generic;

public class RedFlagSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject redFlag;
    [SerializeField] private GameObject[] tracks;
    [SerializeField] private float totalTime;
    private int currentPlayerPosition;
    private int difficulty;
    float precentage = .99f;



    void Start()
    {
        difficulty = Mathf.Min(Mathf.FloorToInt(precentage * 4), 4);
        precentage = GameEvents.GetPercentage?.Invoke() ?? 2f;
    }

    void Update()
    {
        totalTime += Time.deltaTime;
        currentPlayerPosition = player.GetComponent<RunnerPlayerMove>().lastPosition;

        if (totalTime >= 1)
        {
            totalTime -= 1;
            HandleSpawn();
        }
    }

    private void HandleSpawn()
    {
        int randSpawnAmount = Random.Range(2, 4);
        List<int> spawns = new List<int>();

        while (spawns.Count < randSpawnAmount)
        {
            if (spawns.Count == 0)
            {
                SpawnFlag(tracks[currentPlayerPosition]);
                spawns.Add(currentPlayerPosition);
                continue;
            }

            int selectTrack;
            do
            {
                selectTrack = Random.Range(0, tracks.Length);
            } while (spawns.Contains(selectTrack));

            SpawnFlag(tracks[selectTrack]);
            spawns.Add(selectTrack);
        }
    }

    void SpawnFlag(GameObject spawnTrack)
    {
        GameObject flag = Instantiate(redFlag, gameObject.transform);

        RedFlagBehaviour behaviour = flag.GetComponent<RedFlagBehaviour>();

        behaviour.Initialized(spawnTrack, difficulty);
    }
}
