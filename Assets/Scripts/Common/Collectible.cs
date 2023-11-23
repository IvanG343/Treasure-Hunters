using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    [Header("Item Params")]
    [SerializeField] private int itemType;
    [SerializeField] private int value;

    [Header("UI Params")]
    [SerializeField] private Text scoreText;

    [Header("References")]
    private Health playerHealth;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerHealth = GameObject.Find("Hero").GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            switch (itemType)
            {
                case 1:
                    int currentScore = int.Parse(scoreText.text);
                    scoreText.text = (currentScore + value).ToString();
                    animator.SetTrigger("Gather");
                    break;
                case 2:
                    if (playerHealth.currentHealth < playerHealth.maxHealth)
                    {
                        playerHealth.Heal(1);
                        animator.SetTrigger("Gather");
                    }
                    break;
            }
        }
    }

    private void Deactivate()
    {
        Destroy(gameObject);
    }
}
