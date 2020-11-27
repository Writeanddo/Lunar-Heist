using UnityEngine;

public class ReturnToBase : MonoBehaviour
{
    private Vector2 StartPosition;
    public float Speed;
    public SpriteRenderer sprite;

    void Start()
    {
        StartPosition = transform.position;
    }

    void Update()
    {
        sprite.flipX = (transform.position.x - StartPosition.x > 0);
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, StartPosition, step);
    }
}
