using System.Collections;
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
    public float VisionDistance = 4f;
    public float AttackVisionDistance = 4f;
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

    private int[] layersToConsider = new int[2]{ Constants.PLAYER_LAYER, Constants.GROUND_LAYER };

    private float sizeY;

    void Awake()
    {
        sizeY = sprite.size.y;
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

        var rays = RaysDirectionVision(isLookingLeft, VisionDistance);
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

        var leftRays = RaysDirectionVision(true, AttackVisionDistance);
        var rightRays = RaysDirectionVision(false, AttackVisionDistance);

        var rays = new RaycastHit2D[leftRays.Length + rightRays.Length];
        leftRays.CopyTo(rays, 0);
        rightRays.CopyTo(rays, leftRays.Length);

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

    private void AttackInProgress()
    {
        bool collidedWithPlayer = false;
        var rays = new RaycastHit2D[]
       {
                DrawRay(transform.position.x, Vector2.left, transform.position.y, AttackVisionDistance),
                DrawRay(transform.position.x, Vector2.right, transform.position.y, AttackVisionDistance)
       };
        foreach (RaycastHit2D ray in rays)
        {
            if (CollidedWithPlayer(ray))
            {
                collidedWithPlayer = true;
            }
        }

        if (!collidedWithPlayer)
        {
            SetToNeutral();
        }
    }

    private void MoveTowardsTarget(GameObject gameObject)
    {
        float step = AttackSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, gameObject.transform.position, step);

        if (transform.position == gameObject.transform.position)
        {
            UpdateState(EnemyState.ATTACK);
            Respawner.Respawn("caught");
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

    private RaycastHit2D[] RaysDirectionVision(bool isLookingLeft, float distance)
    {
        return new RaycastHit2D[]
         {
                DrawRay(transform.position.x, isLookingLeft ? Vector2.left :  Vector2.right, transform.position.y, distance),
                DrawRay(transform.position.x, isLookingLeft ? new Vector2(-1,0.25f) : new Vector2(1,0.25f), transform.position.y, distance),
                DrawRay(transform.position.x, isLookingLeft ? new Vector2(-1,0.5f) : new Vector2(1,0.5f), transform.position.y, distance),
                DrawRay(transform.position.x, isLookingLeft ? new Vector2(-1,0.4f) : new Vector2(1,0.4f), transform.position.y, distance),
                DrawRay(transform.position.x, isLookingLeft ? new Vector2(-1,-0.4f) : new Vector2(1,0.4f), transform.position.y, distance),
                DrawRay(transform.position.x, isLookingLeft ? new Vector2(-1,-0.25f) : new Vector2(1,0.4f), transform.position.y, distance),
                DrawRay(transform.position.x, isLookingLeft ? Vector2.left :  Vector2.right, transform.position.y - sizeY /2, distance),
                DrawRay(transform.position.x, isLookingLeft ? Vector2.left :  Vector2.right, transform.position.y + sizeY /2, distance),
                DrawRay(transform.position.x, Vector2.up, transform.position.y, 3f),
         };
    }
}
