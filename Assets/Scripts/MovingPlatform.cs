using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public Transform TargetPosition;
    public float speed = 1.0f;

    private Vector2 target;
    private Vector2 initialPosition;
    private Vector2 currentTarget;

    private string direction = "towards";

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

        if (direction == "towards")
        {
            if (transform.position.x >= currentTarget.x && transform.position.y >= currentTarget.y)
            {
                direction = "away";
                currentTarget = initialPosition;
            }
        }else
        {
            if (transform.position.x <= currentTarget.x && transform.position.y <= currentTarget.y)
            {
                direction = "towards";
                currentTarget = target;
            }
        }
    }

}
