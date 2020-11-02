using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Speed;
    public int JumpHeight;
    public Transform GroundCheck;

    private Rigidbody2D Body;
    private Vector2 Inputs;
    private bool IsGrounded;

    void Start()
    {
        Body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        IsGrounded = Physics2D.OverlapBox(GroundCheck.position, new Vector2(1, 1), 0);

        Inputs = Vector2.zero;
        Inputs.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            Body.AddForce(Vector2.up * Mathf.Sqrt(JumpHeight * -2f * Physics2D.gravity.y), ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        Body.MovePosition(Body.position + Inputs * Speed * Time.fixedDeltaTime);
    }
}
