using UnityEngine;
using System.Collections;

public class DisolvablePlatform : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public Material Normal;
    public BoxCollider2D Collider;
    private bool canTrigger = true;

    public AudioSource platRespawnSFX;
    public AudioSource countdownSourceSFX;
    public AudioClip[] countdownSFX;

    public Dissolver Dissolver;

    private IEnumerator DisolveTimer;
    private IEnumerator RespawnPlatformTimer;

    void OnEnable()
    {
        if (DisolveTimer != null)
        {
            StopCoroutine(DisolveTimer);
        }
        if (RespawnPlatformTimer != null)
        {
            StopCoroutine(RespawnPlatformTimer);
        }
        respawn();    
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player" || collision.tag == "Box") && canTrigger)
        {
            canTrigger = false;
            DisolveTimer = StartDisolveTimer();
            RespawnPlatformTimer = RespawnPlatform();
            StartCoroutine(DisolveTimer);
            StartCoroutine(RespawnPlatformTimer);
        }
    }

    IEnumerator StartDisolveTimer()
    {
        countdownSourceSFX.PlayOneShot(countdownSFX.PickRandom());
        yield return new WaitForSeconds(1f);
        Dissolver.Dissolve();
        Collider.enabled = false;
    }

    IEnumerator RespawnPlatform()
    {
        yield return new WaitForSeconds(4f);
        respawn();
    }

    private void respawn()
    {
        Sprite.material = Normal;
        platRespawnSFX.Play();
        Collider.enabled = true;
        canTrigger = true;
    }
}
