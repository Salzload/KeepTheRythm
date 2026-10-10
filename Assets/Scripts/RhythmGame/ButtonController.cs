using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSprite;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public Key key;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        theSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current == null) return;

        if (Keyboard.current[key].wasPressedThisFrame)
        {
            theSprite.sprite = pressedImage;
        }

        if(Keyboard.current[key].wasReleasedThisFrame)
        {
            theSprite.sprite = defaultImage;
        }
    }
}
