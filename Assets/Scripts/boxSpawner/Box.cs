using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour
{
    public SpriteRenderer sprite;
    public Color ColourOnSelect;
    public Color ColourOnHover;
    private Vector3 screenPoint;
    private Vector3 offset;
    public bool drag = false;

    void OnMouseDown()
    {
        sprite.color = ColourOnSelect;
        drag = true;

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
        transform.position = curPosition;
    }

    void OnMouseUp()
    {
        sprite.color = Color.white;
        drag = false;
    }

    void OnMouseOver()
    {
        if (!drag)
        {
            sprite.color = ColourOnHover;
        }
    }


    void OnMouseExit()
    {
        sprite.color = Color.white;
    }
}
