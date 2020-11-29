using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelicopterSound : MonoBehaviour
{
    public AudioSource helicopterSoundSouce;
    public AudioClip helicopterSound;
    public void StartHelicopterSound()
    {
        helicopterSoundSouce.PlayOneShot(helicopterSound);
    }
}
