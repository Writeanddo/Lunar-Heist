using UnityEngine;

public class Moving : MonoBehaviour
{
    public Transform TargetPosition;
    public float speed = 1.0f;
    public bool flipSpriteOnTurn;
    public SpriteRenderer Sprite;
    public BoxCollider2D BoxCollider;

    private Vector2 target;
    private Vector2 initialPosition;
    private Vector2 currentTarget;
    private Rigidbody2D Player;

    public bool mountable;

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
        Vector3 originalPosition = transform.position;
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, step);

        if (mountable) {
            Rigidbody2D Player = BoxCollider.GetPlayerRidingHits();

            if (Player != null)
            {
                Player.position = Player.position + new Vector2(transform.position.x - originalPosition.x, transform.position.y - originalPosition.y);
            }
        }
        
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
        int xDistance = Mathf.RoundToInt(Mathf.Abs(currentTarget.x - transform.position.x));
        int yDistance = Mathf.RoundToInt(Mathf.Abs(currentTarget.y - transform.position.y));

        if (xDistance == 0 && yDistance == 0)
        {
            TurnAround();
        }
    }
    private void AfterMoveAwayFromTarget()
    {
        int xDistance = Mathf.RoundToInt(Mathf.Abs(transform.position.x - currentTarget.x));
        int yDistance = Mathf.RoundToInt(Mathf.Abs(transform.position.y - currentTarget.y));
        if (xDistance == 0 && yDistance == 0)
        {
            TurnAround();
        }
    }

    private void TurnAround()
    {
        direction = (direction == Direction.AWAY) ? Direction.TOWARDS : Direction.AWAY;
        currentTarget = (currentTarget == initialPosition) ? target : initialPosition;
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (Sprite != null && flipSpriteOnTurn)
        {
            Sprite.flipX = !Sprite.flipX;
        }
    }
}
