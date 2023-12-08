using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Fire Params")]
    [SerializeField] private float fireForce;
    [SerializeField] private float attackCooldown;
    private float cooldownTimer;
    private float direction;

    [Header("Detection params")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private float offset;
    [SerializeField] private float colliderSize;
    [SerializeField] private LayerMask playerLayer;

    [Header("References")]
    [SerializeField] private GameObject cannonBall;
    [SerializeField] private Transform firePoint;
    [SerializeField] private AudioClip fireSound;

    private void Start()
    {
        direction = transform.localScale.x;
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown)
            if (PlayerInSight())
                Shoot(direction);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * offset * transform.localScale.x * colliderDistance,
            new Vector2(boxCollider.bounds.size.x * colliderSize, boxCollider.bounds.size.y * (colliderSize/2)),
            0, Vector2.left, 0, playerLayer);

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * offset * transform.localScale.x * colliderDistance,
            new Vector2(boxCollider.bounds.size.x * colliderSize, boxCollider.bounds.size.y * (colliderSize / 2)));
    }

    public void Shoot(float _direction)
    {
        cooldownTimer = 0;
        GameObject newCannonBall = Instantiate(cannonBall, firePoint.position, Quaternion.identity);
        Rigidbody2D newCannonBallVelocity = newCannonBall.GetComponent<Rigidbody2D>();
        newCannonBallVelocity.velocity = new Vector2(fireForce * _direction, newCannonBallVelocity.velocity.y);
        SoundManager.instance.PlaySound(fireSound);
    }
}
