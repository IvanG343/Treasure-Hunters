using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Attack params")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float offset;
    [SerializeField] private int damage;

    [Header("Collder params")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player params")]
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer;

    //References
    private Health playerHealth;
    private Animator animator;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if(PlayerInSight())
        {
            if (cooldownTimer > attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetTrigger("attack");
            }
        }

        if(enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * offset * transform.localScale.x * colliderDistance, 
            new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y),
            0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * offset * transform.localScale.x * colliderDistance,
            new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y));
    }

    private void DamagePlayer()
    {
        if(PlayerInSight())
        {
            if (PlayerInSight())
                playerHealth.TakeDamage(damage);
        }
    }
}
