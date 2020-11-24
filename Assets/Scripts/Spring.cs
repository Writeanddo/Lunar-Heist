using UnityEngine;
using System.Collections;

public class Spring : MonoBehaviour
{
    public Vector2 Force;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.tag == "Player" || collision.tag == "Box"))
        {
            Debug.Log("Collided");
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Force, ForceMode2D.Impulse);
        }
    }
}
