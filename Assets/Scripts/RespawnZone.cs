using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class RespawnZone : MonoBehaviour
{
    public Respawner Respawner;
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
        if (collision.tag == "Player")
        {
            Respawner.SetRespawn(gameObject);
        }
    }
}
