using UnityEngine;

public class CannonBall : MonoBehaviour
{
    private Animator animator;
    private float lifeTime;
    [SerializeField] private float resetTime;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;

        if(lifeTime >= resetTime)
            animator.SetTrigger("Hit");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(1, gameObject.transform);
            animator.SetTrigger("Hit");
        }
        else
        {
            animator.SetTrigger("Hit");
        }
        Debug.Log(collision.gameObject.name);
    }

    private void Deactivate()
    {
        Destroy(gameObject);
    }
}
