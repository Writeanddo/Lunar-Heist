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
            source.PlayOneShot(Character.SpawnOutSounds.PickRandom(), 5f);
        }
    }

    public void SetRespawnIn()
    {
        if (Character.SpawnInSounds.Length > 0)
        {
            source.PlayOneShot(Character.SpawnInSounds.PickRandom(), 1f);
        }
    }

    public void SetDeath()
    {
        if (Character.DeathSounds.Length > 0)
        {
            source.PlayOneShot(Character.DeathSounds.PickRandom());
        }
    }

    public void PlayGrapple()
    {
        if (Character.GrappleSounds.Length > 0)
        {
            source.clip = Character.GrappleSounds.PickRandom();
            source.loop = true;
            source.Play();
        }
    }

    public void StopGrapple()
    {
        source.loop = false;
        source.Stop();
    }
}
