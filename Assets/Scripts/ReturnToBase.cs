using UnityEngine;

public class ReturnToBase : MonoBehaviour
{
    private Vector2 StartPosition;
    public float Speed;
    public SpriteRenderer sprite;
    private bool startFlip;

    void Start()
    {
        StartPosition = transform.position;
        startFlip = sprite.flipX;
    }

    void Update()
    {
        if(transform.position.x - StartPosition.x == 0)
        {
            sprite.flipX = startFlip;
            return;
        }
        sprite.flipX = (transform.position.x - StartPosition.x > 0);
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, StartPosition, step);
    }
}
