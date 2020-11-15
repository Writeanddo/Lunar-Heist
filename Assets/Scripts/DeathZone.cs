using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class DeathZone : MonoBehaviour
{
    public Respawner Respawner;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Respawner.Respawn(null);
        }
    }
}
