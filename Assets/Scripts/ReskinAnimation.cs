using UnityEngine;

public class ReskinAnimation : MonoBehaviour
{
    public Character Character;
    public SpriteRenderer SpriteRenderer;

    private Sprite SpriteToSet;

    void LateUpdate()
    {
        if (SpriteToSet != null)
        {
            SpriteRenderer.sprite = SpriteToSet;
        }
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

    public void SetCaught()
    {
        SpriteToSet = Character.Caught;
    }

    public void SetRespawnOut()
    {
        SpriteToSet = null;
    }
    public void SetRespawnIn()
    {
        SpriteToSet = null;
    }

    public void SetDeath()
    {
        SpriteToSet = Character.Land;
    }
}
