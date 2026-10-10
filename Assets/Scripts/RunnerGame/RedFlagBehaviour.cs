using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class RedFlagBehaviour : MonoBehaviour
{
    GameObject responsibelTrack;
    float highestY;
    float currentX;
    float lifetime;

    Vector2 moveDirection = Vector2.down;
    [SerializeField] float moveSpeed = 6f;
    [SerializeField] float hpToLoose = -10f;

    public void Initialized(GameObject track, int difficulty)
    {
        responsibelTrack = track;
        moveSpeed += (difficulty * 2);
        SetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        if (responsibelTrack == null) return;
        lifetime += Time.deltaTime;

        if (lifetime > 4f)
        {
            Destroy(gameObject);
            return;
        }

        float movement = moveSpeed * Time.deltaTime;

        transform.position += (Vector3)(moveDirection * moveSpeed * Time.deltaTime);
    }

    void SetPosition()
    {
        SpriteRenderer trackSpriteRender = responsibelTrack.GetComponent<SpriteRenderer>();

        highestY = trackSpriteRender.bounds.max.y;
        currentX = trackSpriteRender.bounds.center.x;


        transform.position = new Vector3(
            currentX,
            highestY,
            transform.position.z
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameEvents.OnLoveRemoved?.Invoke(hpToLoose);
            Destroy(gameObject);
            Debug.Log("get good");
            return;
        }
    }
}
