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

    [Header("SFX")]
    [SerializeField] private AudioClip meleeAttackSound;


    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    //Получаем коллайдер противника если он находится в зоне действия удара и наносим урон
    //Вызываем метод, чтобы оттолкнуть противника
    public void Attack()
    {
        animator.SetTrigger("attack");
        Collider2D enemyCollider = Physics2D.OverlapCircle(attackPoint.position, mAttackRange, enemyLayer);
        if(enemyCollider != null && enemyCollider.tag != "Traps")
        {
            KnockbackEnemy(enemyCollider);
            enemyCollider.gameObject.GetComponent<Health>().TakeDamage(damage);
            SoundManager.instance.PlaySound(meleeAttackSound);
        } 
    }

    private void KnockbackEnemy(Collider2D enemyCollider)
    {
        Rigidbody2D enemyRB = enemyCollider.GetComponent<Rigidbody2D>();
        if (transform.position.x < enemyCollider.transform.position.x)
            enemyRB.velocity = new Vector2(3, 2);
        else
            enemyRB.velocity = new Vector2(-3, 2);
    }

    //Задает положение снаряда и вызывает метод для его движения в нужном направлении
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

    //Возвращает индекс следующего снаряда
    private int FindBullet()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }

    //Визуальное отображение зоны удара игрока
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, mAttackRange);
    }
}
