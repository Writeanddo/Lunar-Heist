using UnityEngine;

public class TogglePlatform : MonoBehaviour
{

    public float Interval = 2f;

    public Material OnShader;
    public Material OffShader;

    public Color StartColour;
    public Color EndColour;

    private BoxCollider2D boxColider;
    private SpriteRenderer sprite;
    public bool startOn;
    private float timer;


    void Start()
    {
        boxColider = GetComponentInChildren<BoxCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        OnShader = new Material(OnShader);

        SetOnOrOff(startOn);
    }

    void Update()
    {
        timer += Time.deltaTime;

        OnShader.SetColor("Color_D28C6A9", Color.Lerp(StartColour, EndColour, Mathf.PingPong(Time.time, Interval)));
        if (timer >= Interval)
        {
            timer = 0f;
            SetOnOrOff(!boxColider.enabled);
        }

    }

    private void SetOnOrOff(bool on)
    {
        boxColider.enabled = on;
        sprite.material = on ? OnShader : OffShader;

        Color c = new Color(1, 1, 1, on ? 1 : 0.25f);
        sprite.color = c;
    }
}
