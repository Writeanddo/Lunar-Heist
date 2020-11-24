﻿using UnityEngine;

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
        RaycastHit2D[] hits = Physics2D.BoxCastAll(BoxCollider.transform.position, BoxCollider.size, 0, Vector2.up, 0.5f);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.tag == "Player")
            {
                Player = hits[i].transform.gameObject.GetComponent<Rigidbody2D>();

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
