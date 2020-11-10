using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class Character : ScriptableObject
{
    public Sprite[] Run;  
    public Sprite Stand;
    public Sprite Head;
    public string SceneName;
    public AudioMixerSnapshot audioSnapshot;
}
