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
        Vector2 direction = isHorizontal ? Vector2.right : Vector2.up;

        int count = Rb2d.Cast(direction, Filter, Hits, isHorizontal ? Movement.x : Movement.y);

        for (int i = 0; i < count; i++)
        {
            RaycastHit2D hit = Hits[i];
            float distance = hit.distance;

            if (isHorizontal)
            {
                Movement.x = 0;
            }

            if (!IsJumping)
            {
                Movement.y = 0;
                Velocity.y = 0;
                IsGrounded = true;
            }

            if (distance < 0)
            {
                Movement.y += Mathf.Abs(distance);
            }
        }

        Rb2d.MovePosition(Rb2d.position + Movement);
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
