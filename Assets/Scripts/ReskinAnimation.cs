using UnityEngine;
using System.Collections;

public class ReskinAnimation : MonoBehaviour
{
    public CharacterSpriteHolder Sprites;
    public SpriteRenderer SpriteRenderer;

    private Sprite SpriteToSet;

    void LateUpdate()
    {
        SpriteRenderer.sprite = SpriteToSet;
    }

    public void SetRunning(int index)
    {
        SpriteToSet = Sprites.Run[index];
    }
    public void SetStand()
    {
        SpriteToSet = Sprites.Stand;
    }
}
