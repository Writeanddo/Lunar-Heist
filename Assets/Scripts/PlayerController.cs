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
        IsGrounded = false;

        OverlappingColliders = Physics2D.OverlapBoxAll(BoxCollider.transform.position, BoxCollider.size * transform.localScale, 0, Ground);

        if (OverlappingColliders.Length > 0)
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
