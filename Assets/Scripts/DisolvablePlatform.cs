using UnityEngine;
using System.Collections;

public class DisolvablePlatform : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public Material Normal;
    private Material Disolver;
    public Shader DisolverShader;
    public BoxCollider2D Collider;
    private bool canTrigger = true;
    private float disolving = 0;

    void Start()
    {
        Disolver = new Material(DisolverShader);
    }

    void Update()
    {
        if (disolving > 0)
        {
            Disolver.SetFloat("_Fade", disolving);
            disolving -= 0.005f;
        }
    }

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
        yield return new WaitForSeconds(1f);
        disolving = 0.9f;
        Sprite.material = Disolver;
        Collider.enabled = false;
    }

    IEnumerator RespawnPlatform()
    {
        yield return new WaitForSeconds(4f);
        disolving = 0;
        Sprite.material = Normal;
        Collider.enabled = true;
        canTrigger = true;
    }
}
