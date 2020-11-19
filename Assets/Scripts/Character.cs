using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public Sprite[] Run;  
    public Sprite Stand;
    public Sprite Jump;
    public Sprite Land;
    public Sprite Fall;
    public Sprite Caught;
    public Sprite Grapple;

    public Sprite Head;

    public string SceneName;
    public AudioMixerSnapshot audioSnapshot;

    public AudioClip[] JumpSounds;
    public AudioClip[] LandSounds;
    public AudioClip[] FootstepSounds;
    public AudioClip[] DeathSounds;
    public AudioClip[] CaughtSounds;
    public AudioClip[] SpawnOutSounds;
    public AudioClip[] SpawnInSounds;
    public AudioClip[] GrappleSounds;

}
