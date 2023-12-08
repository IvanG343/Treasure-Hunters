using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [Header("Meele Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float mAttackRange;
    [SerializeField] private float damage;
    [SerializeField] private LayerMask enemyLayer;

    [Header("References")]
    private Animator animator;

    [Header("SFX")]
    [SerializeField] private AudioClip meleeAttackSound;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //ѕолучаем коллайдер противника если он находитс€ в зоне действи€ удара и наносим урон
    public void Attack()
    {
        animator.SetTrigger("attack");
        Collider2D enemyCollider = Physics2D.OverlapCircle(attackPoint.position, mAttackRange, enemyLayer);
        if(enemyCollider != null && enemyCollider.tag != "Traps")
        {
            enemyCollider.gameObject.GetComponent<Health>().TakeDamage(damage, gameObject.transform);
            SoundManager.instance.PlaySound(meleeAttackSound);
        } 
    }

    //¬изуальное отображение зоны удара игрока
    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, mAttackRange);
    }
}
