using System;
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
    private bool isOn;

    public AudioSource toggleSoundSource;
    public AudioClip[] toggleSound;

    void Start()
    {
        boxColider = GetComponentInChildren<BoxCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        OnShader = new Material(OnShader);
        OnShader.SetColor("Color_D28C6A9", StartColour * 3.3f);

        SetOnOrOff(startOn);
    }

    void Update()
    {
        timer += Time.deltaTime;
      

        if (timer >= Interval)
        {
            timer = 0f;
            SetOnOrOff(!isOn);
        }

        if (isOn)
        {
            OnShader.SetColor("Color_D28C6A9", Color.Lerp(StartColour, EndColour, timer / Interval) * 3.3f);
        }

    }

    private void SetOnOrOff(bool on)
    {
        isOn = on;
        boxColider.enabled = on;
        sprite.material = on ? OnShader : OffShader;
        toggleSoundSource.PlayOneShot(toggleSound.PickRandom());

        if (!on)
        {
            sprite.color = new Color(1, 1, 1, 0.25f);
        }
    }
}
