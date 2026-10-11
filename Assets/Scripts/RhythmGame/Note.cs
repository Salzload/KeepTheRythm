using UnityEngine;

public class Note : MonoBehaviour
{
    public Conductor conductor;
    public int lane;
    public float beat;

    public float beatsShownInAdvance = 2f;
    
    public float startY;
    public float targetY;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conductor = FindAnyObjectByType<Conductor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(conductor == null) return;
        
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
