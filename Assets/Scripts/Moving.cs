using UnityEngine;

public class Moving : MonoBehaviour
{
    public Transform TargetPosition;
    public float speed = 1.0f;
    public bool flipSpriteOnTurn;
    public SpriteRenderer Sprite;

    private Vector2 target;
    private Vector2 initialPosition;
    private Vector2 currentTarget;


    private enum Direction
    {
        TOWARDS,
        AWAY
    }

    private Direction direction = Direction.TOWARDS;

    void Start()
    {
        initialPosition = transform.position;
        target = TargetPosition.position;
        currentTarget = target;
    }

    void FixedUpdate()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);

        switch (direction)
        {
            case Direction.TOWARDS:
                AfterMoveTowardsTarget();
                break;
            case Direction.AWAY:
                AfterMoveAwayFromTarget();
                break;
        }
    }

    private void AfterMoveTowardsTarget()
    {
        if (transform.position.x >= currentTarget.x && transform.position.y >= currentTarget.y)
        {
            direction = Direction.AWAY;
            FlipSprite();
            currentTarget = initialPosition;
        }
    }
    private void AfterMoveAwayFromTarget()
    {
        if (transform.position.x <= currentTarget.x && transform.position.y <= currentTarget.y)
        {
            direction = Direction.TOWARDS;
            FlipSprite();
            currentTarget = target;
        }
    }

    private void FlipSprite()
    {
        if (Sprite != null && flipSpriteOnTurn)
        {
            Sprite.flipX = !Sprite.flipX;
        }
    }
}
