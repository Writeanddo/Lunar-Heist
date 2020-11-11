using UnityEngine;

public class ReskinAnimation : MonoBehaviour
{
    public Character Character;
    public SpriteRenderer SpriteRenderer;

    private Sprite SpriteToSet;

    void LateUpdate()
    {
        SpriteRenderer.sprite = SpriteToSet;
    }

    public void SetRunning(int index)
    {
        SpriteToSet = Character.Run[index];
    }

    public void SetStand()
    {
        SpriteToSet = Character.Stand;
    }

    public void SetJump()
    {
        SpriteToSet = Character.Jump;
    }

    public void SetLand()
    {
        SpriteToSet = Character.Land;
    }

    public void SetFall()
    {
        SpriteToSet = Character.Fall;
    }
}
