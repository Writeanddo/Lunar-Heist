using UnityEngine;

public class GraplingHook : MonoBehaviour
{
    private Collider2D player;
    public SpriteRenderer MouseSprite;
    public Color HighlightColour;
    public SpriteRenderer Hook;

    void Start()
    {
        MouseSprite.enabled = false;
        Hook.color = HighlightColour;
    }

    void Update()
    {
        if (player != null && Input.GetButtonDown("Fire1"))
        {
            player.gameObject.GetComponentInChildren<PlayerGraplingHook>().Graple(transform.position);
            MouseSprite.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision;
            MouseSprite.enabled = true;
            Hook.color = Color.white; ;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = null;
            MouseSprite.enabled = false;
            Hook.color = HighlightColour;
        }
    }
}
