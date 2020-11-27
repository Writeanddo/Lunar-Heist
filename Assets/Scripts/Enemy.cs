using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private enum EnemyState
    {
        NEUTRAL,
        TARGETING,
        ATTACK,
        SLEEP
    }

    public float AttackSpeed;
    public SpriteRenderer sprite;
    public MonoBehaviour Moving;
    public Sprite AttackSprite;
    public Sprite NeutralSprite;
    public Sprite SleepSprite;
    private EnemyState enemyState = EnemyState.NEUTRAL;
    public Respawner Respawner;
    public SpriteRenderer Highlight;

    public AudioSource randomRoboDeath;
    public AudioSource RobotPatrol;
    public AudioClip[] roboDeathSFX;
    public BoxCollider2D Collider;

    private int[] layersToConsider = new int[2]{ Constants.PLAYER_LAYER, Constants.GROUND_LAYER };
    private int layerMask;

    private ContactFilter2D noFilter = new ContactFilter2D().NoFilter();

    void Awake()
    {
        layerMask = RayHelper.GetLayerMask(layersToConsider);
    }

    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.NEUTRAL:
                PlayerSneakAttack();
                break;
        }
    }

    void FixedUpdate()
    {
        switch (enemyState)
        {
            case EnemyState.NEUTRAL:
                NeutralLookAround();
                break;
            case EnemyState.TARGETING:
                bool shouldContinuingAttacking = TargetingLookAround();
                if (shouldContinuingAttacking)
                {
                    return;
                }
                SetToNeutral();
                break;
            case EnemyState.ATTACK:
                AttackInProgress();
                break;
            case EnemyState.SLEEP:
                break;
        }
    }

    private void NeutralLookAround()
    {
        bool isLookingLeft = sprite.flipX;

        var rays = RaysDirectionVision(isLookingLeft);
        foreach (RaycastHit2D ray in rays)
        {
            if (CollidedWithPlayer(ray))
            {
                UpdateState(EnemyState.TARGETING);
                sprite.sprite = AttackSprite;
                UpdateAttachedScript(false);
                MoveTowardsTarget(ray.collider.gameObject);
            }
        }
    }

    private void PlayerSneakAttack()
    {
        bool isLookingLeft = sprite.flipX;

        var rays = new RaycastHit2D[]
         {
                DrawRay(transform.position.x, isLookingLeft ? Vector2.right :  Vector2.left, transform.position.y, 2f)
         };
        foreach (RaycastHit2D ray in rays)
        {
            if (CollidedWithPlayer(ray))
            {
                Highlight.gameObject.SetActive(true);
                if (Input.GetButtonUp("Fire1")) {
                    UpdateState(EnemyState.SLEEP);
                    UpdateAttachedScript(false);
                    sprite.sprite = SleepSprite;
                    RobotPatrol.Stop();
                    randomRoboDeath.clip = roboDeathSFX[Random.Range(0, roboDeathSFX.Length)];
                    randomRoboDeath.Play();
                }
            }
            else
            {
                Highlight.gameObject.SetActive(false);
            }
        }
    }

    private bool TargetingLookAround()
    {

        var leftRays = RaysDirectionVision(true);
        var rightRays = RaysDirectionVision(false);

        leftRays.AddRange(rightRays);

        foreach (RaycastHit2D ray in leftRays)
        {
            if (CollidedWithPlayer(ray))
            {
                MoveTowardsTarget(ray.collider.gameObject);
                return true;
            }
        }

        return false;
    }

    private void AttackInProgress()
    {
       
    }

    private void MoveTowardsTarget(GameObject gameObject)
    {
        float step = AttackSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, gameObject.transform.position, step);

        if (playerRightNextToRobot())
        {
            UpdateState(EnemyState.ATTACK);
            Respawner.Respawn("caught", SetToNeutral);
        }

    }

    private RaycastHit2D DrawRay(float x, Vector2 direction, float yStart, float rayLength)
    {
        Vector2 origin = new Vector2(x, yStart);

        return RayHelper.DrayRay(origin, direction, rayLength, transform, layersToConsider);
    }

    private void UpdateAttachedScript(bool enable)
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

    private void SetToNeutral()
    {
        UpdateState(EnemyState.NEUTRAL);
        sprite.sprite = NeutralSprite;
        UpdateAttachedScript(true);
    }

    private void UpdateState(EnemyState state)
    {
        enemyState = state;
        Highlight.gameObject.SetActive(false);
    }

    private List<RaycastHit2D> RaysDirectionVision(bool isLookingLeft)
    {
        return new List<RaycastHit2D>
        {
            cast(isLookingLeft ? Vector2.left : Vector2.right),
            cast(isLookingLeft ? new Vector2(-1, 0.25f) : new Vector2(1, 0.25f)),
            cast(isLookingLeft ? new Vector2(-1, 0.5f) : new Vector2(1, 0.5f)),
            cast(isLookingLeft ? new Vector2(-1, 0.75f) : new Vector2(1, 0.75f)),
            cast(isLookingLeft ? new Vector2(-1, -0.25f) : new Vector2(1, -0.25f)),
            cast(isLookingLeft ? new Vector2(-1, -0.5f) : new Vector2(1, -0.5f)),
            cast(isLookingLeft ? new Vector2(-1, -0.75f) : new Vector2(1, -0.75f)),
            cast(Vector2.up, 5f),
            cast(Vector2.down, 5f)
        };
    }

    private bool playerRightNextToRobot()
    {
        bool onRobot = playerIsOnRobot();

        if (onRobot) return true;

        float width = Collider.size.x;
        var collided = new List<RaycastHit2D>
        {
            cast(Vector2.left, width),
            cast(Vector2.right, width),
            cast(Vector2.up, width),
            cast(Vector2.down, width),
            cast(new Vector2(1, 0.5f), width),
            cast(new Vector2(1, -0.5f), width),
            cast(new Vector2(-1, -0.5f), width),
            cast(new Vector2(-1, 0.5f), width),
        };

        foreach (RaycastHit2D ray in collided)
        {
            if (CollidedWithPlayer(ray))
            {
                return true;
            }
        }
        return false;
    }

    private RaycastHit2D cast(Vector2 direction, float size= Mathf.Infinity)
    {
        var position = Collider.transform.position;

        return Physics2D.BoxCast(position, Collider.size, 0, direction, size, layerMask);
    }

    private bool playerIsOnRobot()
    {
        var directlyOnPlayerColliders = new List<Collider2D>();
        Physics2D.OverlapCollider(Collider, noFilter, directlyOnPlayerColliders);
        foreach (Collider2D collider in directlyOnPlayerColliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                return true;
            }
        }
        return false;
    }
}
