using UnityEngine;

public class PlayerGraplingHook : MonoBehaviour
{
    public PlayerController Controller;
    public Transform Player;
    public Color Colour;
    public Material LineMaterial;
    public Animator Animator;
    public SpriteRenderer Sprite;

    private Vector2 target;
    private float speed = 20f;
    private GameObject Line;

    public CharacterSoundPlayer CharacterSoundPlayer;

    void OnEnable()
    {
        CharacterSoundPlayer.StopGrapple();
    }

    void FixedUpdate()
    {
        if (target !=Vector2.zero)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                DoneGrapling();
                return;
            }
           

            float step = speed * Time.deltaTime;
            Player.position =Vector2.MoveTowards(Player.position, target, step);
            if (Player.position.x >= target.x && Player.position.y >= target.y)
            {
                DoneGrapling();
                return;
            }
            GameObject.Destroy(Line);
            DrawLine();
        }
    }

    private void DoneGrapling()
    {
        CharacterSoundPlayer.StopGrapple();
        target = Vector2.zero;
        Controller.enabled = true;
        GameObject.Destroy(Line);
        Animator.SetBool("grapple", false);
    }

    public void Graple(Vector2 target)
    {
        CharacterSoundPlayer.PlayGrapple();
        if (Line != null)
        {
            GameObject.Destroy(Line);
        }
        this.target = target;
        Controller.enabled = false;
        DrawLine();
        Animator.SetBool("grapple", true);
        Sprite.flipX = Player.transform.position.x - target.x > 0;

    }

    void DrawLine()
    {
       Vector2 start = Player.transform.position;
        Vector2 end = target;
        Line = new GameObject();
        Line.transform.position = start;
        Line.AddComponent<LineRenderer>();
        LineRenderer lr = Line.GetComponent<LineRenderer>();
        lr.material = LineMaterial;
        lr.startColor = Colour;
        lr.endColor = Colour;
        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.sortingOrder = 100;
    }
}
