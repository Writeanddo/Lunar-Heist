using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieSound : MonoBehaviour
{
    public AudioSource runSoundSouce;
    public AudioClip[] runningSound;
    public void StartRunSound()
    {
        runSoundSouce.PlayOneShot(runningSound.PickRandom());
    }
}
