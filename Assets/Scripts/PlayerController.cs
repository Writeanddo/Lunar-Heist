using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int Speed;
    public int JumpHeight;

    private Rigidbody2D _body;
    private Vector2 _inputs;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _inputs = Vector2.zero;
        _inputs.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
            _body.AddForce(Vector2.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode2D.Impulse);
        }
    }

    void FixedUpdate()
    {
        _body.MovePosition(_body.position + _inputs * Speed * Time.fixedDeltaTime);
    }
}
