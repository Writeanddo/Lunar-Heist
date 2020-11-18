using UnityEngine;
using System.Collections;

public class BlockerEnabler : MonoBehaviour
{
    public string text;
    public BlockerName Name;
    private Collider2D player;
    private SpriteRenderer sprite;
    private BlockerManager BlockerManager;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        BlockerManager = FindObjectOfType<BlockerManager>();
    }

    void Update()
    {
        if (player != null && Input.GetButtonUp("Submit"))
        {
            player.gameObject.GetComponentInChildren<PlayerDialogue>().setText(text);

            sprite.enabled = false;
            BlockerManager.Blockers[Name] = true;
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
