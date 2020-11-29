using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TogglePlatform : MonoBehaviour
{

    public float Interval = 2f;
    public float Offset = 0f;

    public Material OnShader;
    public Material OffShader;

    private BoxCollider2D boxColider;
    private SpriteRenderer sprite;
    public bool startOn;
    private float timer;

    private bool doneOffset = false;

    void Start()
    {
        boxColider = GetComponentInChildren<BoxCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        SetOnOrOff(startOn);

        doneOffset = Offset == 0;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!doneOffset)
        {
            if (timer >= Offset)
            {
                timer = 0f;
                SetOnOrOff(!boxColider.enabled);
                doneOffset = true;
            }
        }
        else
        {
            if (timer >= Interval)
            {
                timer = 0f;
                SetOnOrOff(!boxColider.enabled);
            }
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
