using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float currentHealth { get; private set; }
    private Animator animator;
    private bool isAlive = true;

    private void Awake()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if(currentHealth > 0)
        {
            animator.SetTrigger("hurt");
        }
        else
        {
            if(isAlive)
            {
                animator.SetTrigger("die");

                //Player
                if(GetComponent<PlayerInput>() != null)
                    GetComponent<PlayerInput>().enabled = false;

                //Enemy
                if(GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;

                if (GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;

                isAlive = false;
            }
        }
    }
}
