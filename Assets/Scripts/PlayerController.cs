using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Speed;
    public int JumpHeight;
    public Rigidbody2D Rb2d;
    public BoxCollider2D BoxCollider;
    public LayerMask Ground;

    private int GravityModifier = 10;
    private Vector2 Velocity;
    private Vector2 Movement;
    private bool IsGrounded;
    private bool IsJumping;
    private Collider2D[] OverlappingColliders;

    public AudioSource randomJumpSound;
    public AudioSource[] jumpSounds;

    void Update()
    {
        Velocity.x = Input.GetAxisRaw("Horizontal") * Speed;

        if (Input.GetButtonDown("Jump") && IsGrounded)
        {
            Velocity.y = Mathf.Sqrt(-2 * Physics2D.gravity.y * JumpHeight);
            if (jumpSounds.Length > 0)
            {
                randomJumpSound = jumpSounds[Random.Range(0, jumpSounds.Length)];
                randomJumpSound.Play();
            }
        
            IsJumping = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            IsJumping = false;
        }
    }

    void FixedUpdate()
    {
        Velocity += GravityModifier * Physics2D.gravity * Time.deltaTime;

        Movement = Velocity * Time.deltaTime;

        IsGrounded = false;

        OverlappingColliders = Physics2D.OverlapBoxAll(BoxCollider.transform.position, BoxCollider.size * transform.localScale, 0, Ground);

        for (int i = 0; i < OverlappingColliders.Length; i++)
        {
            Collider2D collider = OverlappingColliders[i];
            float distance = collider.Distance(BoxCollider).distance;

            if (distance < 0)
            {
                Movement.y += Mathf.Abs(distance);
            }

            if (!IsJumping)
            {
                Movement.y = 0;
                IsGrounded = true;
            }
        }

        Rb2d.MovePosition(Rb2d.position + Movement);
    }
}
