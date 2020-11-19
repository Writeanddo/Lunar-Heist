using UnityEngine;

public class GraplingHook : MonoBehaviour
{
    private Collider2D player;
    public SpriteRenderer HoverSprite;

    void Start()
    {
        HoverSprite.enabled = false;
    }

    void Update()
    {
        if (player != null && Input.GetButtonDown("Fire1"))
        {
            player.gameObject.GetComponentInChildren<PlayerGraplingHook>().Graple(transform.position);
            HoverSprite.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision;
            HoverSprite.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = null;
            HoverSprite.enabled = false;
        }
    }
}
