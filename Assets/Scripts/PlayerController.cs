using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Speed;
    public int JumpHeight;
    public LayerMask Ground;
    public BoxCollider2D BoxCollider;

    private Vector2 Velocity;
    private bool IsGrounded;
    private Collider2D[] OverlappingColliders;

    /*
     *  s = ut + 0.5 * a * t^2
     *  s = ut if a = 0
     *  v^2 = u^2 + 2 * a * s
     *  - 2 * a * s = u^2
     *  sqrt(-2 * g * s) = u
     */

    void Update()
    {
        if (IsGrounded)
        {
            Velocity.y = 0;
        }
        else
        {
            Velocity.y += Physics2D.gravity.y * Time.deltaTime;
        }

        Velocity.x = Input.GetAxisRaw("Horizontal") * Speed;

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            Velocity.y = Mathf.Sqrt(-2 * Physics2D.gravity.y * JumpHeight);
        }

        OverlappingColliders = Physics2D.OverlapBoxAll(BoxCollider.transform.position, BoxCollider.size * transform.localScale, 0, Ground);

        IsGrounded = false;

        for (int i = 0; i < OverlappingColliders.Length; i++)
        {
            IsGrounded = true;
        }

        transform.Translate(Velocity * Time.deltaTime);
    }
}
