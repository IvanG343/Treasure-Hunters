using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image currentHP;
    private float divider;

    private void Start()
    {
        divider = playerHealth.maxHealth;
    }

    private void Update()
    {
        currentHP.fillAmount = playerHealth.currentHealth / divider;
    }
}
