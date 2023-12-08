using System.Collections;
using UnityEngine;

public class KnockbackEffect : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private PlayerInput playerInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void Knockback(Transform sender)
    {
        if(playerInput != null)
            StartCoroutine(PlayerControl());
        if (transform.position.x < sender.transform.position.x)
            rb.velocity = new Vector2(-3, 5);
        else
            rb.velocity = new Vector2(3, 5);
    }

    private IEnumerator PlayerControl()
    {
        playerInput.enabled = false;
        yield return new WaitForSeconds(0.3f);
        playerInput.enabled = true;
    }
}
