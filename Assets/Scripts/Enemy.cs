using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float AttackSpeed;
    public float VisionDistance = 4f;
    public SpriteRenderer sprite;
    public Moving Moving;
    public Sprite AttackSprite;
    public Sprite NeutralSprite;
    private bool targeting = false;

    private float sizeY;

    void Awake()
    {
        sizeY = sprite.size.y;
    }

    void FixedUpdate()
    {
        if (targeting)
        {
            bool shouldReturn = TargetingLookAround();
            if (shouldReturn)
            {
                return;
            }
            targeting = false;
            sprite.sprite = NeutralSprite;
            UpdateMoving(true);
        }
        else
        {
             NeutralLookAround();
        }
    }

    private void NeutralLookAround()
    {
        bool isLookingLeft = sprite.flipX;

        var rays = new RaycastHit2D[]
         {
                DrawRay(transform.position.x, isLookingLeft ? Vector2.left :  Vector2.right )
         };
        foreach (RaycastHit2D ray in rays)
        {
            if (CollidedWithPlayer(ray))
            {
                targeting = true;
                sprite.sprite = AttackSprite;
                UpdateMoving(false);
                MoveTowardsTarget(ray.collider.gameObject);
            }
        }
    }

    private bool TargetingLookAround()
    {
        var rays = new RaycastHit2D[]
         {
                DrawRay(transform.position.x, Vector2.left),
                DrawRay(transform.position.x, Vector2.right)
         };
        foreach (RaycastHit2D ray in rays)
        {
            if (CollidedWithPlayer(ray))
            {
                MoveTowardsTarget(ray.collider.gameObject);
                return true;
            }
        }

        return false;
    }

    private void MoveTowardsTarget(GameObject gameObject)
    {
        float step = AttackSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, gameObject.transform.position, step);

    }

    private RaycastHit2D DrawRay(float x, Vector2 direction)
    {
        float yStart = transform.position.y;
        Vector2 origin = new Vector2(x, yStart);

        return RayHelper.DrayRay(origin, direction, VisionDistance, transform, Constants.PLAYER_LAYER);
    }

    private void UpdateMoving(bool enable)
    {
        if (Moving != null && Moving.enabled != enable)
        {
            Moving.enabled = enable;
        }
    }

    private bool CollidedWithPlayer(RaycastHit2D ray)
    {
        return ray.collider != null && ray.collider.gameObject.tag == "Player";
    }
}
