using UnityEngine;

public class CharacterSoundPlayer : MonoBehaviour
{
    public AudioSource source;
    public Character Character;

    public void SetRunning(int index)
    {
        if (Character.FootstepSounds.Length > 0 && index % 4 == 0)
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
            source.PlayOneShot(Character.LandSounds.PickRandom(), 2.5f);
        }
    }

    public void SetCaught()
    {
        if (Character.CaughtSounds.Length > 0)
        {
            source.PlayOneShot(Character.CaughtSounds.PickRandom());
        }
    }


    public void SetRespawnOut()
    {
        if (Character.SpawnOutSounds.Length > 0)
        {
            source.PlayOneShot(Character.SpawnOutSounds.PickRandom());
        }
    }

    public void SetRespawnIn()
    {
        if (Character.SpawnInSounds.Length > 0)
        {
            source.PlayOneShot(Character.SpawnInSounds.PickRandom());
        }
    }

    public void SetDeath()
    {
        if (Character.DeathSounds.Length > 0)
        {
            source.PlayOneShot(Character.DeathSounds.PickRandom());
        }
    }

    public void SetGrapple()
    {
        if (Character.DeathSounds.Length > 0)
        {
            source.PlayOneShot(Character.GrappleSounds.PickRandom());
        }
    }
}
