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
    public SpriteRenderer sprite;
    public Moving Moving;
    public Sprite AttackSprite;
    public Sprite NeutralSprite;
    public Sprite SleepSprite;
    private EnemyState enemyState = EnemyState.NEUTRAL;
    public Respawner Respawner;
    public SpriteRenderer Highlight;

    public AudioSource randomRoboDeath;
    public AudioClip[] roboDeathSFX;

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

        var rays = new RaycastHit2D[]
         {
                DrawRay(transform.position.x, isLookingLeft ? Vector2.left :  Vector2.right )
         };
        foreach (RaycastHit2D ray in rays)
        {
            if (CollidedWithPlayer(ray))
            {
                UpdateState(EnemyState.TARGETING);
                sprite.sprite = AttackSprite;
                UpdateMoving(false);
                MoveTowardsTarget(ray.collider.gameObject);
            }
        }
    }

    private void PlayerSneakAttack()
    {
        bool isLookingLeft = sprite.flipX;

        var rays = new RaycastHit2D[]
         {
                DrawRay(transform.position.x, isLookingLeft ? Vector2.right :  Vector2.left )
         };
        foreach (RaycastHit2D ray in rays)
        {
            if (CollidedWithPlayer(ray))
            {
                Highlight.gameObject.SetActive(true);
                if (Input.GetButtonUp("Submit")) {
                    UpdateState(EnemyState.SLEEP);
                    UpdateMoving(false);
                    sprite.sprite = SleepSprite;
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

    private void AttackInProgress()
    {
        bool collidedWithPlayer = false;
        var rays = new RaycastHit2D[]
       {
                DrawRay(transform.position.x, Vector2.left),
                DrawRay(transform.position.x, Vector2.right)
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

    private void SetToNeutral()
    {
        UpdateState(EnemyState.NEUTRAL);
        sprite.sprite = NeutralSprite;
        UpdateMoving(true);
    }

    private void UpdateState(EnemyState state)
    {
        enemyState = state;
        Highlight.gameObject.SetActive(false);
    }
}
