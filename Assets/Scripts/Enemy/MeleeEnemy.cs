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

    [Header("SFX")]
    [SerializeField] private AudioClip meleeAttackSound;

    [Header("References")]
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

        if(PlayerInSight() && playerHealth.isAlive && !playerHealth.isInv)
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
            playerHealth = hit.transform.GetComponent<Health>();

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
        if (PlayerInSight() && !playerHealth.isInv)
        {
            playerHealth.TakeDamage(damage, gameObject.transform);
            SoundManager.instance.PlaySound(meleeAttackSound);
        }
    }

    //Наносит урон игроку при столкновении
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.gameObject.GetComponent<Health>().TakeDamage(damage, gameObject.transform);
            
    }
}
