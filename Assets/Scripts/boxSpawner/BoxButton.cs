using UnityEngine;

public class BoxButton : MonoBehaviour
{
    public BoxSpawner BoxSpawner;
    public Color ButtonOnHoverColour;
    public SpriteRenderer Sprite;
    public GameObject ClickSprite;

    void OnMouseDown()
    {
        BoxSpawner.SpawnBox();
    }

    void OnMouseOver()
    {
        Sprite.color = ButtonOnHoverColour;
        ClickSprite.SetActive(true);

    }


    void OnMouseExit()
    {
        Sprite.color = Color.white;
        ClickSprite.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ClickSprite.SetActive(true);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ClickSprite.SetActive(false);
        }
    }


}
