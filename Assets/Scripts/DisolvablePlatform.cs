using UnityEngine;
using System.Collections;

public class DisolvablePlatform : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public Material Normal;
    public BoxCollider2D Collider;
    private bool canTrigger = true;

    public AudioSource dissolveSFX;
    public AudioSource platRespawnSFX;
    public AudioSource countdownSourceSFX;
    public AudioClip[] countdownSFX;

    public Dissolver Dissolver;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player" || collision.tag == "Box") && canTrigger)
        {
            canTrigger = false;
            StartCoroutine(StartDisolveTimer());
            StartCoroutine(RespawnPlatform());
        }
    }

    IEnumerator StartDisolveTimer()
    {
        countdownSourceSFX.PlayOneShot(countdownSFX.PickRandom());
        yield return new WaitForSeconds(1f);
        Dissolver.Dissolve();
        dissolveSFX.Play();
        Collider.enabled = false;
    }

    IEnumerator RespawnPlatform()
    {
        yield return new WaitForSeconds(4f);
        Sprite.material = Normal;
        platRespawnSFX.Play();
        Collider.enabled = true;
        canTrigger = true;
    }
}
