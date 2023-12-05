using UnityEngine;

public class ShipController : MonoBehaviour
{
    [Header("Ship params")]
    private Animator sailAnimator;
    private Transform ship;
    [SerializeField] private float speed;
    [SerializeField] private Transform leftEdge;
    private BoxCollider2D boxCollider;

    [Header("Player")]
    [SerializeField] private PlayerInput input;
    [SerializeField] private Animator playerAnimator;

    public bool levelComplete;
    private bool playerOnBoard;

    private void Awake()
    {
        ship = GetComponent<Transform>();
        sailAnimator = GetComponentInChildren<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.parent = transform;
        playerOnBoard = true;
        input.enabled = false;

        playerAnimator.SetBool("isRunning", false);
        sailAnimator.SetTrigger("start");

        GameManager.instance.LevelComplete();
    }

    private void Update()
    {
        if(playerOnBoard && transform.position.x >= leftEdge.position.x)
        {
            ship.position = new Vector2(ship.position.x + Time.deltaTime * -1 * speed, ship.position.y);
        }
    }

    public void SetLevelComplete()
    {
        boxCollider.enabled = true;
        levelComplete = true;
    }

}
