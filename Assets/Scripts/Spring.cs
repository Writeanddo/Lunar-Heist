using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
{
    public Vector2 Force;
    public int XVelocity;
    public float YVelocity;

    public AudioSource bounceSoundSource;
    public AudioClip[] bounceSound;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Box")
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Force, ForceMode2D.Impulse);
            bounceSoundSource.PlayOneShot(bounceSound.PickRandom());
        } 
        else if (collision.tag == "Player")
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();

            playerController.Velocity = new Vector2(playerController.Velocity.x, YVelocity);
            playerController.Speed =  XVelocity;
            bounceSoundSource.PlayOneShot(bounceSound.PickRandom());
        }
    }
}
