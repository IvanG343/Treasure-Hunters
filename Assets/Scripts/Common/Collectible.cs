using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Item Params")]
    //1 - Treasures; 2 - Health potions; 3 - Map; 4 - Golden Skull;
    [SerializeField] private int itemType;
    //Coin cost; Map pieces;
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
                    itemCollider.enabled = false;
                    GameManager.instance.AddScore(gameObject.tag, value);
                    animator.SetTrigger("Collected");
                    SoundManager.instance.PlaySound(pickupSound);
                    break;
                case 2:
                    if (playerHealth.currentHealth < playerHealth.maxHealth)
                    {
                        itemCollider.enabled = false;
                        playerHealth.Heal(1);
                        animator.SetTrigger("Collected");
                        SoundManager.instance.PlaySound(pickupSound);
                    }
                    break;
                case 3:
                    itemCollider.enabled = false;
                    GameManager.instance.CollectMapPieces(value);
                    animator.SetTrigger("Collected");
                    SoundManager.instance.PlaySound(pickupSound);
                    break;
            }
        }
    }

    private void Deactivate()
    {
        Destroy(gameObject);
    }
}
