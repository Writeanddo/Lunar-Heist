using UnityEngine;

public class ReskinAnimation : MonoBehaviour
{
    public Character Character;
    public SpriteRenderer SpriteRenderer;
    public CharacterSoundPlayer CharacterSoundPlayer;

    private Sprite SpriteToSet;

    void LateUpdate()
    {
        SpriteRenderer.sprite = SpriteToSet;
    }

    public void SetRunning(int index)
    {
        SpriteToSet = Character.Run[index];
        CharacterSoundPlayer.SetRunning(index);
    }

    public void SetStand()
    {
        SpriteToSet = Character.Stand;
    }

    public void SetJump()
    {
        SpriteToSet = Character.Jump;
        CharacterSoundPlayer.SetJump();
    }

    public void SetLand()
    {
        SpriteToSet = Character.Land;
        CharacterSoundPlayer.SetLand();
    }

    public void SetFall()
    {
        SpriteToSet = Character.Fall;
    }
}
