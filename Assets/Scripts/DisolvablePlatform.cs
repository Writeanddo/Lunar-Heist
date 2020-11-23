using UnityEngine;
using System.Collections;

public class DisolvablePlatform : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public Material Normal;
    public Material Disolver;
    public BoxCollider2D Collider;
    private bool canTrigger = true;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && canTrigger)
        {
            canTrigger = false;
            StartCoroutine(StartDisolveTimer());
            StartCoroutine(RespawnPlatform());
        }
    }

    IEnumerator StartDisolveTimer()
    {
        yield return new WaitForSeconds(1f);
        Sprite.material = Disolver;
        Collider.enabled = false;
    }

    IEnumerator RespawnPlatform()
    {
        yield return new WaitForSeconds(4f);
        Sprite.material = Normal;
        Collider.enabled = true;
        canTrigger = true;
    }
}
