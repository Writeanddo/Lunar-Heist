using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Speed;
    public int JumpHeight;
    public LayerMask Ground;

    private Vector2 Velocity;
    private bool IsGrounded;
    private RaycastHit2D[] hits;

    /*
     *  s = ut + 0.5 * a * t^2
     *  s = ut if a = 0
     *  v^2 = u^2 + 2 * a * s
     *  - 2 * a * s = u^2
     *  sqrt(-2 * g * s) = u
     */

    void Update()
    {
        IsGrounded = false;

        hits = Physics2D.BoxCastAll(new Vector2(transform.position.x, transform.position.y), new Vector2(1, 1), 0f, Vector2.down, 1f, Ground);

        if (hits.Length > 0)
        {
            IsGrounded = true;
            Debug.Log(IsGrounded);
        }

        Velocity.x = Input.GetAxisRaw("Horizontal") * Speed;

        if (Input.GetButtonDown("Jump"))
        {
            Velocity.y += Mathf.Sqrt(-2 * Physics2D.gravity.y * JumpHeight);
        }

        if (IsGrounded)
        {
            Velocity.y = 0;
        }
        else
        {
            Velocity.y += Physics2D.gravity.y * Time.deltaTime;
        }

        transform.Translate(Velocity * Time.deltaTime);
    }
}
