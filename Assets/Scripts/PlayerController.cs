using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Speed;
    public int JumpHeight;

    private Rigidbody2D Body;
    private Vector2 Inputs;

    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Inputs = Vector2.zero;
        Inputs.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            Body.AddForce(Vector2.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        Body.MovePosition(Body.position + Inputs * Speed * Time.fixedDeltaTime);
    }
}
