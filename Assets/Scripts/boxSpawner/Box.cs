using System.Collections;
using UnityEngine;

public class Box : MonoBehaviour
{
    public SpriteRenderer sprite;
    public BoxCollider2D boxCollider2D;
    public Color ColourOnSelect;
    public Color ColourOnHover;
    private Vector3 screenPoint;
    private Vector3 offset;
    public bool drag = false;

    public BoxCollider2D BoxBounds;
    public BoxSpawner BoxSpawner;
    public Dissolver Dissolver;

  
    void Update()
    {
        if (outOfBounds())
        {
            BoxWentOutOfBounds();
        }
    }

    void OnMouseDown()
    {
        sprite.color = ColourOnSelect;
        drag = true;

        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        if (!outOfBounds() && drag)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }else
        {
            stopDragging();
            BoxWentOutOfBounds();
        }

    }

    void OnMouseUp()
    {
        stopDragging();
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

    private bool outOfBounds()
    {
        return !BoxBounds.IsTouching(boxCollider2D);
    }

    private void stopDragging()
    {
        sprite.color = Color.white;
        drag = false;
    }

    private void BoxWentOutOfBounds()
    {
        Dissolver.Dissolve();
        StartCoroutine(StartDisolveTimer());
    }

    IEnumerator StartDisolveTimer()
    {
        yield return new WaitForSeconds(4f);
        BoxSpawner.SpawnBox();
    }

}
