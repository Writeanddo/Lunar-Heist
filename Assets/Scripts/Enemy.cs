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
    public float vision = 20f;
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
    private List<Collider2D> directlyOnPlayerColliders = new List<Collider2D>();

    private RaycastHit2D emptyRay = new RaycastHit2D();
    private RaycastHit2D[] leftRays;
    private RaycastHit2D[] rightRays;
    private RaycastHit2D[] closeRays;

    private int[] layersToConsider = new int[2] { Constants.PLAYER_LAYER, Constants.GROUND_LAYER };
    private int layerMask;

    private ContactFilter2D noFilter = new ContactFilter2D().NoFilter();


    void Start()
    {
        leftRays = new RaycastHit2D[9] { emptyRay, emptyRay, emptyRay, emptyRay, emptyRay, emptyRay, emptyRay, emptyRay, emptyRay };
        rightRays = new RaycastHit2D[7] { emptyRay, emptyRay, emptyRay, emptyRay, emptyRay, emptyRay, emptyRay };
        closeRays = new RaycastHit2D[8] { emptyRay, emptyRay, emptyRay, emptyRay, emptyRay, emptyRay, emptyRay, emptyRay };
    }

    void Awake()
    {
        layerMask = RayHelper.GetLayerMask(layersToConsider);
    }

    void FixedUpdate()
    {
        switch (enemyState)
        {
            case EnemyState.NEUTRAL:
                NeutralLookAround();
                PlayerSneakAttack();
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
        RaycastHit2D[] rays;

        if (isLookingLeft)
        {
            rays = RaysDirectionVisionLeft();
        }
        else
        {
            rays = RaysDirectionVisionRight();
        }

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

        var ray = DrawRay(transform.position.x, isLookingLeft ? Vector2.right : Vector2.left, transform.position.y, 2f);

        if (CollidedWithPlayer(ray))
        {
            Highlight.gameObject.SetActive(true);
            if (Input.GetButtonUp("Fire1"))
            {
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

    private bool TargetingLookAround()
    {

        var leftRays = RaysDirectionVisionLeft();
        var rightRays = RaysDirectionVisionRight();

        foreach (RaycastHit2D ray in leftRays)
        {
            if (CollidedWithPlayer(ray))
            {
                MoveTowardsTarget(ray.collider.gameObject);
                return true;
            }
        }

        foreach (RaycastHit2D ray in rightRays)
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

    private RaycastHit2D[] RaysDirectionVisionLeft()
    {
        leftRays[0] = cast(Vector2.left, vision);
        leftRays[1] = cast(new Vector2(-1, 0.25f), vision);
        leftRays[2] = cast(new Vector2(-1, 0.5f), vision);
        leftRays[3] = cast(new Vector2(-1, 0.75f), vision);
        leftRays[4] = cast(new Vector2(-1, -0.25f), vision);
        leftRays[5] = cast(new Vector2(-1, -0.5f), vision);
        leftRays[6] = cast(new Vector2(-1, -0.75f), vision);
        leftRays[7] = cast(Vector2.up, 5f);
        leftRays[8] = cast(Vector2.down, 5f);

        return leftRays;
    }

    private RaycastHit2D[] RaysDirectionVisionRight()
    {
        rightRays[0] = cast(Vector2.right, vision);
        rightRays[1] = cast(new Vector2(1, 0.25f), vision);
        rightRays[2] = cast(new Vector2(1, 0.5f), vision);
        rightRays[3] = cast(new Vector2(1, 0.75f), vision);
        rightRays[4] = cast(new Vector2(1, -0.25f), vision);
        rightRays[5] = cast(new Vector2(1, -0.5f), vision);
        rightRays[6] = cast(new Vector2(1, -0.75f), vision);
        return rightRays;
    }

    private bool playerRightNextToRobot()
    {
        bool onRobot = playerIsOnRobot();

        if (onRobot) return true;

        float width = Collider.size.x;

        closeRays[0] = cast(Vector2.left, width);
        closeRays[1] = cast(Vector2.right, width);
        closeRays[2] = cast(Vector2.up, width);
        closeRays[3] = cast(Vector2.down, width);
        closeRays[4] = cast(new Vector2(1, 0.5f), width);
        closeRays[5] = cast(new Vector2(1, -0.5f), width);
        closeRays[6] = cast(new Vector2(-1, -0.5f), width);
        closeRays[7] = cast(new Vector2(-1, 0.5f), width);

        foreach (RaycastHit2D ray in closeRays)
        {
            if (CollidedWithPlayer(ray))
            {
                return true;
            }
        }
        return false;
    }

    private RaycastHit2D cast(Vector2 direction, float size)
    {
        var position = Collider.transform.position;

        return Physics2D.BoxCast(position, Collider.size, 0, direction, size, layerMask);
    }

    private bool playerIsOnRobot()
    {
        directlyOnPlayerColliders.Clear();
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
