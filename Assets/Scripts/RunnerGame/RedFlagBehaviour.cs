using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class RedFlagBehaviour : MonoBehaviour
{
    GameObject responsibelTrack;
    GameObject selfRedFlag;
    float highestY;
    float currentX;
    float lifetime;

    Vector2 moveDirection = Vector2.down;
    [SerializeField] float moveSpeed = 20f;

    public void Initialized(GameObject track, GameObject self)
    {
        responsibelTrack = track;
        selfRedFlag = self;
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
}
