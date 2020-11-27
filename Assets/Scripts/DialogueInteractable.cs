using UnityEngine;
using System.Collections;

public class DialogueInteractable : MonoBehaviour
{
    public string text;
    private Collider2D player;
    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
    }

    void Update()
    {
        if (player != null && Input.GetButtonUp("Fire1"))
        {
            player.gameObject.GetComponentInChildren<PlayerDialogue>().setText(text);
            sprite.enabled = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = collision;
            sprite.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player = null;
            sprite.enabled = false;
        }
    }
}
