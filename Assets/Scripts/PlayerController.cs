using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Speed;
    public int JumpHeight;
    public Rigidbody2D Rb2d;
    public BoxCollider2D BoxCollider;
    public LayerMask Ground;
    public Animator Animator;
    public SpriteRenderer Sprite;

    private int GravityModifier = 10;
    private Vector2 Velocity;
    private Vector2 Movement;
    private bool IsGrounded;
    private bool IsJumping;
    private RaycastHit2D[] Hits = new RaycastHit2D[50];
    private ContactFilter2D Filter;

    void Start()
    {
        Filter = new ContactFilter2D();
        Filter.layerMask = Ground;
    }

    void OnEnable()
    {
        Velocity = Vector2.zero;
        IsJumping = false;
    }

    void Update()
    {
        Velocity.x = Input.GetAxisRaw("Horizontal") * Speed;

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            Velocity.y = Mathf.Sqrt(-2 * Physics2D.gravity.y * JumpHeight);
            IsJumping = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (Velocity.y > 0)
            {
                Velocity.y = Velocity.y * 0.5f;
            }
            IsJumping = false;
        }
    }

    void FixedUpdate()
    {
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;

        Movement = Velocity * Time.deltaTime;

        IsGrounded = false;

        Move(true);
        Move(false);

        UpdateAnimations();
    }

    void Move(bool isHorizontal)
    {
        Vector2 direction;
        float displacement;

        if (isHorizontal)
        {
            direction = Velocity.x > 0  ? Vector2.right : Vector2.left;
            displacement = Mathf.Abs(Movement.x);
        }
        else
        {
            direction = Velocity.y > 0 ? Vector2.up : Vector2.down;
            displacement = Mathf.Abs(Movement.y);
        }

        int count = Rb2d.Cast(direction, Filter, Hits, displacement + 0.01f);

        for (int i = 0; i < count; i++)
        {
            RaycastHit2D hit = Hits[i];
            float distance = hit.distance;

            if (hit.normal.y == 1)
            {
                IsGrounded = true;
                Velocity.y = 0;
            }

            displacement = distance - 0.01f < displacement ? distance - 0.01f : displacement;
        }

        if (isHorizontal)
        {
            if (Velocity.x < 0)
            {
                Rb2d.position = Rb2d.position + Vector2.left * displacement;
            }
            else
            {
                Rb2d.position = Rb2d.position + Vector2.right * displacement;
            }
        }
        else
        {
            if (Velocity.y  <= 0)
            {
                Rb2d.position = Rb2d.position + Vector2.down * displacement;
            }
            else
            {
                Rb2d.position = Rb2d.position + Vector2.up * displacement;
            }
        }

    }

    private void UpdateAnimations()
    {
        if (Velocity.x > 0)
        {
            Sprite.flipX = false;
        }

        if (Velocity.x < 0)
        {
            Sprite.flipX = true;
        }

        Animator.SetInteger("xVelocity", Mathf.FloorToInt(Velocity.x));
        Animator.SetInteger("yVelocity", Mathf.FloorToInt(Velocity.y));
        Animator.SetBool("isGrounded", IsGrounded);
    }
}
