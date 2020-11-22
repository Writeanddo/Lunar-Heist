using UnityEngine;

public class Platform : MonoBehaviour
{
    private Transform oldParent;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            oldParent = collision.transform.parent;
            collision.transform.SetParent(transform);
        }
    }


    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.transform.SetParent(oldParent);
        }
    }
}
