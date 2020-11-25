using UnityEngine;

public class BoxButton : MonoBehaviour
{
    public BoxSpawner BoxSpawner;
    public Color ButtonOnHoverColour;
    public SpriteRenderer Sprite;

    void OnMouseDown()
    {
        BoxSpawner.SpawnBox();
    }

    void OnMouseOver()
    {
        Sprite.color = ButtonOnHoverColour;
    }


    void OnMouseExit()
    {
        Sprite.color = Color.white;
    }

}
