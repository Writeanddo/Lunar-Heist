using UnityEngine;
using UnityEngine.Events;

public class EndDoor : MonoBehaviour
{
    public Character Character;
    public GameObject Player;
    public string text;
    public UnityEvent onInteract;
    public SpriteRenderer sprite;

    private bool collided;

    void Update()
    {
        if(collided && Input.GetButtonUp("Fire1"))
        {
            sprite.enabled = false;
            Player.GetComponent<PlayerController>().FreezeInputs();
            if (onInteract != null)
            {
                onInteract.Invoke();
            }
            Player.GetComponentInChildren<PlayerDialogue>().setText(text, done);
        }
    }

    private void done()
    {
        FindObjectOfType<CharacterSwitcherController>().SetCharacterComplete(Character);

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collided = true;
            sprite.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collided = false;
            sprite.enabled = false;
        }
    }
}
