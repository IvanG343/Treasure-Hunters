using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile Params")]
    [SerializeField] private float fireForce;
    private float direction;
    private bool hit;
    private float damage;
    private float lifetime;

    private BoxCollider2D boxCollider;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = fireForce * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        //Деактивирует снаряд если время жизни больше установленного
        lifetime += Time.deltaTime;
        if(lifetime > 5) gameObject.SetActive(false);
    }

    //Проверка на столкновение с другим коллайдером, если это противник, то наносит урон и деактивируется
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;

        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(damage);
        Deactivate();
    }

    //Задаёт стартовые параметры для снаряда
    public void SetDirection(float _direction, float _damage) 
    {
        lifetime = 0;
        direction = _direction;
        damage = _damage;
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
