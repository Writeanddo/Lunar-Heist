using UnityEngine;

public class CharacterSlot : MonoBehaviour
{
    private Character character;
    private CharacterSwitcherController controller;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Setup(Character character, CharacterSwitcherController controller, bool isSelected, Color disabledColour)
    {
        this.character = character;
        this.controller = controller;
        spriteRenderer.sprite = character.Head;

        if (isSelected)
        {
            ActivateSelected(disabledColour);
        }
        else
        {
            Hide();
        }
    }

    public void ActivateSelected(Color disabledColour)
    {
        spriteRenderer.color = disabledColour;
    }


    public void Preview()
    {
        spriteRenderer.color = Color.white;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        Debug.Log("Select " + character.name);
        controller.SelectCharacter(character);
    }
}
