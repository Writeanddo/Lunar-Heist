using UnityEngine;

public class ReturnToBase : MonoBehaviour
{
    private Vector2 StartPosition;
    public float Speed;

    void Start()
    {
        StartPosition = transform.position;
    }

    void Update()
    {
        float step = Speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, StartPosition, step);
    }
}
