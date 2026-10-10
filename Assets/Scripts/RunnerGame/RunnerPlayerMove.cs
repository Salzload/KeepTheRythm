using UnityEngine;
using UnityEngine.InputSystem;

public class RunnerPlayerMove : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Key leftKey = Key.W;
    [SerializeField] private Key rightKey = Key.R;

    float[] positions = { -6f, -2f, 2f, 6f};
    int lastPosition;

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
            Debug.Log("W gedrückt");

            lastPosition = (lastPosition - 1 + positions.Length) % positions.Length;

            Debug.Log(lastPosition);

            UpdatePlayerPosition();
        }

        if (Keyboard.current[rightKey].wasPressedThisFrame)
        {
            Debug.Log("R gedrückt");

            lastPosition = (lastPosition + 1) % positions.Length;

            Debug.Log(lastPosition);

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
