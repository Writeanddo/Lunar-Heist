using UnityEngine;

public class AvoidPlayer : MonoBehaviour
{
    public BoxCollider2D BoxCollider;

    private Moving MovingComponent;

    void Start()
    {
        MovingComponent = GetComponent<Moving>();
    }

    void FixedUpdate()
    {
        RaycastHit2D[] colliders = Physics2D.BoxCastAll(BoxCollider.transform.position + new Vector3(BoxCollider.offset.x, BoxCollider.offset.y, 0), BoxCollider.size, 0, Vector2.down, 0.5f);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].transform.gameObject.tag == "Player")
            {
                MovingComponent.TurnAround();
            }
        }
    }
}
