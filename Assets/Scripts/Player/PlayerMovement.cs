using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Params")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] LayerMask groundLayer;

    [Header("References")]
    private Rigidbody2D body;
    private Animator animator;
    private CapsuleCollider2D capsuleCollider;

    [Header("Knockback effect")]
    [SerializeField] private float kbForce;
    public float kbCounter;
    public float kbTotalTime;
    public bool knockFromRight;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    //������� �������� ������ � ������ (���������� �� ������� PlayerInput)
    public void MovePlayer(float direction, bool jumpPressed)
    {
        //�������� �� ����������� � ������ ������������ ��� ��������� �����
        if (kbCounter <= 0)
        {
            body.velocity = new Vector2(direction * speed, body.velocity.y);
            animator.SetBool("isRunning", direction != 0);
        }
        else
        {
            if (knockFromRight)
                body.velocity = new Vector2(-kbForce, kbForce);
            else
                body.velocity = new Vector2(kbForce, kbForce);
            kbCounter -= Time.deltaTime;
        }

        //���� ������� ��� ���������
        if (direction > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //������
        if (jumpPressed && IsGrounded())
            Jump();
        animator.SetBool("grounded", IsGrounded());
    }

    //������� ������
    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        animator.SetTrigger("jump");
    }

    //������� �������� ���� ����� ����� �� �����
    public bool IsGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(capsuleCollider.bounds.center, capsuleCollider.bounds.size, capsuleCollider.direction,
            0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
}
