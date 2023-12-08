using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public float maxHealth;
    public float currentHealth { get; private set; }
    public bool isAlive = true;

    [Header("Invulnerability")]
    [SerializeField] private float invDuration;
    [SerializeField] private int flashes;
    private SpriteRenderer playerSprite;
    public bool isInv { get; private set; }

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
        playerSprite = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if(currentHealth > 0)
        {
            animator.SetTrigger("hurt");
            StartCoroutine(Invulnerability());
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
                {
                    GameManager.instance.LevelFailed();
                }
                    
            }
        }
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        isInv = true;
        for (int i = 0; i < flashes; i++)
        {
            playerSprite.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(invDuration / (flashes * 2));
            playerSprite.color = Color.white;
            yield return new WaitForSeconds(invDuration / (flashes * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
        isInv = false;
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
