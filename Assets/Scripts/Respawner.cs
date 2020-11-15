using System.Collections;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    public GameObject[] Zones;
    public GameObject Player;

    private Animator PlayerAnimator;

    void Start()
    {
        PlayerAnimator = Player.GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void Respawn(string animationTrigger)
    {
        if (animationTrigger != null)
        {
            PlayerAnimator.SetTrigger(animationTrigger);
            StartCoroutine(RespawnPlayer());
        }
    }

    IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(1f);
        Vector2 respawnPosition = Zones[0].gameObject.transform.position;
        Player.transform.position = new Vector3(respawnPosition.x, respawnPosition.y, Player.transform.position.z);
        PlayerAnimator.SetTrigger("respawn");
    }
}
