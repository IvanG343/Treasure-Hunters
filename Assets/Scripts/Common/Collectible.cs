using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Item Params")]
    [SerializeField] private int itemType;
    [SerializeField] private int value;

    [SerializeField] private AudioClip pickupSound;

    [Header("References")]
    private Collider2D itemCollider;
    private Health playerHealth;
    private Animator animator;

    private void Awake()
    {
        itemCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        playerHealth = GameObject.Find("Hero").GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            switch (itemType)
            {
                case 1:
                    GameManager.instance.AddScore(gameObject.tag, value);
                    animator.SetTrigger("Collected");
                    SoundManager.instance.PlaySound(pickupSound);
                    break;
                case 2:
                    if (playerHealth.currentHealth < playerHealth.maxHealth)
                    {
                        playerHealth.Heal(1);
                        animator.SetTrigger("Collected");
                        SoundManager.instance.PlaySound(pickupSound);
                    }
                    break;
                case 3:
                    GameManager.instance.CollectMapPieces();
                    animator.SetTrigger("Collected");
                    SoundManager.instance.PlaySound(pickupSound);
                    break;
            }
            itemCollider.enabled = false;
        }
    }

    private void Deactivate()
    {
        Destroy(gameObject);
    }
}
