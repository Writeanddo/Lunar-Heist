using UnityEngine;
using System.Collections;

public class BlockerEnabler : MonoBehaviour
{
    public string text;
    public BlockerName Name;
    private Collider2D player;
    private SpriteRenderer sprite;
    private BlockerManager BlockerManager;
    public GameObject item;


    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
        BlockerManager = FindObjectOfType<BlockerManager>();
    }

    void Update()
    {
        if (player != null && Input.GetButtonUp("Fire1"))
        {
            player.gameObject.GetComponentInChildren<PlayerDialogue>().setText(text);

            sprite.enabled = false;
            BlockerManager.Blockers[Name] = true;
            gameObject.SetActive(false);

            if (item != null)
            {
                item.SetActive(false);
            }
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
