using UnityEngine;

public class Note : MonoBehaviour
{
    public Conductor conductor;
    private SpriteRenderer sprite;
    public Sprite noteLane1;
    public Sprite noteLane2;
    public Sprite noteLane3;
    public Sprite noteLane4;
    public int lane;
    public float beat;

    public float beatsShownInAdvance = 2f;
    
    public float startY;
    public float targetY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conductor = FindAnyObjectByType<Conductor>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(conductor == null) return;
        
        switch(lane)
        {
            case 0:
                sprite.sprite = noteLane1;
                break;
            case 1:
                sprite.sprite = noteLane2;
                break;
            case 2:
                sprite.sprite = noteLane3;
                break;
            case 3:
                sprite.sprite = noteLane4;
                break;
        }

        float progress = (conductor.songPosInBeats - (beat - beatsShownInAdvance)) / beatsShownInAdvance;

        transform.position = new Vector3(
            transform.position.x, 
            Mathf.LerpUnclamped(startY, targetY, progress), 
            transform.position.z
        );

        if(conductor.songPosInBeats > beat + 0.3f)
        {
            Destroy(gameObject);
        }
    }
}
