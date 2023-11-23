using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [Header ("Attack params")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float offset;
    [SerializeField] private int damage;
    private float cooldownTimer;

    [Header("Collder params")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player params")]
    [SerializeField] private LayerMask playerLayer;

    [Header("References")]
    private Health playerHealth;
    private PlayerMovement playerMovement;
    private Transform playerTransform;
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

        if(PlayerInSight() && playerHealth.isAlive)
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

    //Проверка если персонаж в поле зрения врага
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * offset * transform.localScale.x * colliderDistance, 
            new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y),
            0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
        { 
            playerHealth = hit.transform.GetComponent<Health>();
            playerMovement = hit.transform.GetComponent<PlayerMovement>();
            playerTransform = hit.transform;
        }

        return hit.collider != null;
    }

    //Отображение поля зрения врага
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * offset * transform.localScale.x * colliderDistance,
            new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y));
    }

    //Вызывается из анимации удара, если персонаж всё ещё в поле зрения врага то наносит урон
    //Вызывает эффект кнокбэка у игрока
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerMovement.kbCounter = playerMovement.kbTotalTime;
            if(playerTransform.position.x <= transform.position.x)
                playerMovement.knockFromRight = true;
            else
                playerMovement.knockFromRight = false;
            playerHealth.TakeDamage(damage);
        } 
    }

    //Наносит урон игроку при столкновении
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            collision.GetComponent<Health>().TakeDamage(damage);
    }
}
