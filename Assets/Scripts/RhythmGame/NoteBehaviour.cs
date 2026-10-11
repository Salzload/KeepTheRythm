using UnityEngine;

public class NoteBehaviour : MonoBehaviour
{
    Vector2 targetPosition;
    float travelTime;
    Vector2 startPosition;
    float elapsedTime = 0f;

    public void Initialized(float beat4, GameObject track)
    {
        travelTime = beat4;

        Renderer trackRenderer = track.GetComponent<Renderer>();

        Bounds bounds = trackRenderer.bounds;

        startPosition = new Vector2(
            bounds.center.x,
            bounds.min.y
        );

        targetPosition = new Vector2(
            bounds.center.x,
            bounds.max.y
        );

        transform.position = startPosition;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime <= travelTime)
        {
            float progress = elapsedTime / travelTime;

            transform.position = Vector2.Lerp(
                startPosition,
                targetPosition,
                progress
            );
        }
        else
        {
            transform.position += Vector3.up * 5f * Time.deltaTime;

            if (transform.position.y >= targetPosition.y + 1f)
            {
                Destroy(gameObject);
            }
        }
    }
}
