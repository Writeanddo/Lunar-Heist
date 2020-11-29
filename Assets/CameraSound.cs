using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSound : MonoBehaviour
{
    public AudioSource cameraSoundSouce;
    public AudioClip[] cameraSound;
    public void TurnSound()
    {
        cameraSoundSouce.PlayOneShot(cameraSound.PickRandom());
    }
}
