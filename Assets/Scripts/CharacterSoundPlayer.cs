﻿using UnityEngine;

public class CharacterSoundPlayer : MonoBehaviour
{
    public AudioSource source;
    public Character Character;

    public void SetRunning(int index)
    {
        if (Character.FootstepSounds.Length > 0)
        {
            source.PlayOneShot(Character.FootstepSounds.PickRandom());
        }
    }
 

    public void SetJump()
    {
        if (Character.JumpSounds.Length > 0)
        {
            source.PlayOneShot(Character.JumpSounds.PickRandom());
        }
    }

    public void SetLand()
    {
        if (Character.LandSounds.Length > 0)
        {
            source.PlayOneShot(Character.LandSounds.PickRandom());
        }
    }
}
