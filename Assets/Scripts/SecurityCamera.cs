using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    private List<Collider2D> colliding = new List<Collider2D>();
    private ContactFilter2D safeLayerFilter = new ContactFilter2D();
    private bool isCollidingWithPlayer;
    private Collider2D playerCollider;
    public LayerMask layerMask;
    public Respawner Respawner;
    private bool wait = false;

    void Start()
    {
        safeLayerFilter.useTriggers = true;
        safeLayerFilter.SetLayerMask(layerMask);
    }

    void Update()
    {
        if (wait) return;
        if (isCollidingWithPlayer)
        {
            playerCollider.OverlapCollider(safeLayerFilter, colliding);

            if (colliding.Count == 0)
            {
                wait = true;
                Respawner.Respawn("caught", OnRespawn);
            }
        }
    }

    void OnRespawn()
    {
        wait = false;

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isCollidingWithPlayer = true;
            playerCollider = collision;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isCollidingWithPlayer = false;
    }

}
