using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnFX : MonoBehaviour
{
    public AudioSource buttonFx;
    public AudioClip hoverFx;
    public AudioSource clickFx;

    public void HoverSound()
    {
        buttonFx.PlayOneShot(hoverFx);
    }

    public void ClickSound()
    {
        clickFx.Play();
    }
}
