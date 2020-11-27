using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Respawner : MonoBehaviour
{
    public GameObject Player;

    private Animator PlayerAnimator;

    public GameObject LastZone;

    void Start()
    {
        PlayerAnimator = Player.GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void Respawn(string animationTrigger, UnityAction onRespawn = null)
    {
        if (animationTrigger != null)
        {
            PlayerAnimator.SetTrigger(animationTrigger);
        }
        StartCoroutine(RespawnPlayer(onRespawn));
    }

    IEnumerator RespawnPlayer(UnityAction onRespawn )
    {
        yield return new WaitForSeconds(1f);
        Vector2 respawnPosition = LastZone.gameObject.transform.position;
        Player.transform.position = new Vector3(respawnPosition.x, respawnPosition.y, Player.transform.position.z);
        PlayerAnimator.SetTrigger("respawn");
        if (onRespawn != null)
        {
            onRespawn();
        }
    }

    public void SetRespawn(GameObject obj)
    {
        LastZone = obj;
    }
}
