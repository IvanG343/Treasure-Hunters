using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health playerHealth;
    [SerializeField] private Image maxHP;
    [SerializeField] private Image currentHP;

    private void Start()
    {
        maxHP.fillAmount = playerHealth.currentHealth / 10;
    }

    private void Update()
    {
        currentHP.fillAmount = playerHealth.currentHealth / 10;
    }
}
