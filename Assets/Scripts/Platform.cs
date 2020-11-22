using UnityEngine;

public class Platform : MonoBehaviour
{
    public BoxCollider2D BoxCollider;

    private Transform oldParent;

    void FixedUpdate()
    {
        Collider2D[] OverlappingColliders = Physics2D.OverlapBoxAll(BoxCollider.transform.position, BoxCollider.size, 0);

        for (int i = 0; i < OverlappingColliders.Length; i++)
        {
            Collider2D collider = OverlappingColliders[i];

            if (collider.tag == "Player")
            {
                Rigidbody2D rb2d = collider.gameObject.GetComponent<Rigidbody2D>();
                ColliderDistance2D distance = collider.Distance(BoxCollider);

                rb2d.position = rb2d.position + distance.normal * distance.distance;
            }
        }
    }

    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.tag == "Player")
    //     {
    //         oldParent = collision.transform.parent;
    //         collision.transform.SetParent(transform);
    //     }
    // }


    // void OnTriggerExit2D(Collider2D collision)
    // {
    //     if (collision.tag == "Player")
    //     {
    //         collision.transform.SetParent(oldParent);
    //     }
    // }
}
