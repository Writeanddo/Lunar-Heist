using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    public SpriteRenderer sprite;
    public BoxCollider2D boxCollider2D;
    public Color ColourOnSelect;
    public Color ColourOnHover;

    public BoxCollider2D BoxBounds;
    public BoxSpawner BoxSpawner;
    public Dissolver Dissolver;

    private Vector3 offset;
    private bool drag = false;
    private bool outOfBoundsHandled;

    private List<Collider2D> results = new List<Collider2D>();
    private ContactFilter2D filter = new ContactFilter2D().NoFilter();
    public bool mountable = false;

    public AudioSource boxDespawnSoundSource;
    public AudioClip[] boxDespawnSound;

    void OnEnable()
    {
        if (outOfBoundsHandled && outOfBounds())
        {
            Dissolver.StopDissolve();
            outOfBoundsHandled = false;
            BoxSpawner.SpawnBox();
        }    
    }

    void Update()
    {
        if (outOfBounds())
        {
            BoxWentOutOfBounds();
        }
    }

    void FixedUpdate()
    {

        if (mountable)
        {
            Vector3 originalPosition = transform.position;
            Rigidbody2D Player = boxCollider2D.GetPlayerRidingHits(4f);

            if (Player != null)
            {
                Player.position += new Vector2(transform.position.x - originalPosition.x, transform.position.y - originalPosition.y - 0.05f);
            }
        }
    }

    void OnMouseDown()
    {
        sprite.color = ColourOnSelect;
        drag = true;

        offset = gameObject.transform.position - UnityEngine.Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    }

    void OnMouseDrag()
    {
        if (!outOfBounds() && drag)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            Vector3 curPosition = UnityEngine.Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
            transform.position = curPosition;
        }else
        {
            stopDragging();
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
        results.Clear();
        Physics2D.OverlapCollider(boxCollider2D, filter, results);

        return !results.Contains(BoxBounds);
    }

    private void stopDragging()
    {
        sprite.color = Color.white;
        drag = false;
    }

    private void BoxWentOutOfBounds()
    {
        if (!outOfBoundsHandled)
        {

            outOfBoundsHandled = true;
            Dissolver.Dissolve();
            StartCoroutine(StartDisolveTimer());
            boxDespawnSoundSource.PlayOneShot(boxDespawnSound.PickRandom());
        }
    }

    IEnumerator StartDisolveTimer()
    {
        yield return new WaitForSeconds(1f);
        Dissolver.StopDissolve();
        outOfBoundsHandled = false;
        BoxSpawner.SpawnBox();
    }

}
