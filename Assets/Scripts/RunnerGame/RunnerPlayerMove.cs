using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerPlayerMove : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Key leftKey = Key.W;
    [SerializeField] private Key rightKey = Key.R;

    float[] positions = { -6f, -2f, 2f, 6f };
    public int lastPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int startTrack = Random.Range(0, 3);
        lastPosition = startTrack;
        player.transform.position = new Vector3(
            positions[startTrack],
            player.transform.position.y,
            player.transform.position.z
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current[leftKey].wasPressedThisFrame)
        {
            lastPosition = (lastPosition - 1 + positions.Length) % positions.Length;

            UpdatePlayerPosition();
        }

        if (Keyboard.current[rightKey].wasPressedThisFrame)
        {
            lastPosition = (lastPosition + 1) % positions.Length;

            UpdatePlayerPosition();
        }
    }

    private void UpdatePlayerPosition()
    {
        player.transform.position = new Vector3(
            positions[lastPosition],
            player.transform.position.y,
            player.transform.position.z
        );
    }
}
