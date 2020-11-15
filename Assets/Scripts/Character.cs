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

    public Sprite Head;

    public string SceneName;
    public AudioMixerSnapshot audioSnapshot;

    public AudioClip[] JumpSounds;
    public AudioClip[] LandSounds;
    public AudioClip[] FootstepSounds;

}
