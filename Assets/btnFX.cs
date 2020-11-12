using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class btnFX : MonoBehaviour
{
    public AudioSource hoverFx;
    public AudioSource clickFx;

    public void HoverSound()
    {
        hoverFx.Play();
    }

    public void ClickSound()
    {
        clickFx.Play();
    }
}
