using UnityEngine;

public class Respawner : MonoBehaviour
{
    public GameObject[] Zones;
    public GameObject Player;

    void Start()
    {

    }

    void Update()
    {

    }

    public void Respawn()
    {
        Vector2 respawnPosition = Zones[0].gameObject.transform.position;
        Player.transform.position = new Vector3(respawnPosition.x, respawnPosition.y, Player.transform.position.z);
    }
}
