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

    //�������� ���� �������� � ���� ������ �����
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * offset * transform.localScale.x * colliderDistance, 
            new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y),
            0, Vector2.left, 0, playerLayer);

        if(hit.collider != null)
        {
            GetCollisionComponents(hit.collider);
        }

        return hit.collider != null;
    }

    //����������� ���� ������ �����
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * offset * transform.localScale.x * colliderDistance,
            new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y));
    }

    //���������� �� �������� �����, ���� �������� �� ��� � ���� ������ ����� �� ������� ����
    //�������� ������ �������� � ������
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            Knockback();
            playerHealth.TakeDamage(damage);
            SoundManager.instance.PlaySound(meleeAttackSound);
        }
    }

    //������� ���� ������ ��� ������������
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Collider2D collider = collision.gameObject.GetComponent<Collider2D>();
            GetCollisionComponents(collider);
            Knockback();
            playerHealth.TakeDamage(damage);
        }
    }

    private void GetCollisionComponents(Collider2D collisionObject)
    {
        playerHealth = collisionObject.transform.GetComponent<Health>();
        playerMovement = collisionObject.transform.GetComponent<PlayerMovement>();
        playerTransform = collisionObject.transform;
    }

    private void Knockback()
    {
        playerMovement.kbCounter = playerMovement.kbTotalTime;
        if (playerTransform.position.x <= transform.position.x)
            playerMovement.knockFromRight = true;
        else
            playerMovement.knockFromRight = false;
    }
}
