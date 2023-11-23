using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Meele Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float mAttackRange;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask enemyLayer;

    [Header("Range Attack")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float attackCooldown;
    private float cooldownTimer;

    [Header("References")]
    private PlayerMovement playerMovement;
    private Animator animator;


    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    //ѕолучаем коллайдер противника если он находитс€ в зоне действи€ удара и наносим урон
    public void Attack()
    {
        animator.SetTrigger("attack");
        Collider2D enemyCollider = Physics2D.OverlapCircle(attackPoint.position, mAttackRange, enemyLayer);
        if(enemyCollider != null)
            enemyCollider.gameObject.GetComponent<Health>().TakeDamage(damage);
    }

    //«адает положение снар€да и вызывает метод дл€ его движени€ в нужном направлении
    public void RangeAttack()
    {
        if(cooldownTimer > attackCooldown && playerMovement.IsGrounded())
        {
            animator.SetTrigger("rangeAttack");
            cooldownTimer = 0;

            projectiles[FindBullet()].transform.position = firePoint.position;
            projectiles[FindBullet()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x), bulletDamage);
        }
    }

    //¬озвращает индекс следующего снар€да
    private int FindBullet()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    //¬изуальное отображение зоны удара игрока
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, mAttackRange);
    }
}
