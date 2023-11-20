using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;

    private PlayerMovement playerMovement;
    private float cooldownTimer;
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

    public void Attack()
    {
        if(cooldownTimer > attackCooldown && playerMovement.IsGrounded())
        {
            animator.SetTrigger("rangeAttack");
            cooldownTimer = 0;

            projectiles[FindBullet()].transform.position = firePoint.position;
            projectiles[FindBullet()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
        }
    }

    private int FindBullet()
    {
        for (int i = 0; i < projectiles.Length; i++)
        {
            if (!projectiles[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
