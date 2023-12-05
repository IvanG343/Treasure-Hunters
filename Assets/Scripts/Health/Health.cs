using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float maxHealth;
    public float currentHealth { get; private set; }
    public bool isAlive = true;

    [Header("SFX")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip dieSound;

    [Header ("Components")]
    [SerializeField] private Behaviour[] components;
    private Animator animator;

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
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if(isAlive)
            {
                isAlive = false;
                animator.SetTrigger("die");
                SoundManager.instance.PlaySound(dieSound);

                foreach(Behaviour component in components)
                    component.enabled = false;
                if(gameObject.tag == "Player")
                    GameManager.instance.LevelFailed();
            }
        }
    }

    public void Heal(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
