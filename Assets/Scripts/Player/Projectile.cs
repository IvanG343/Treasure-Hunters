using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float fireForce;
    private float direction;
    private bool hit;
    private float lifetime;

    private Animator animator;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = fireForce * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifetime += Time.deltaTime;
        if(lifetime > 5) gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger("embedded");

        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1);
    }

    public void SetDirection(float _direction) 
    {
        lifetime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != direction)
            localScaleX = -localScaleX;
        transform.localScale = new Vector2(localScaleX, transform.localScale.y);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }


}
