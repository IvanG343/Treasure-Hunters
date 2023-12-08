using UnityEngine;

public class WeigthPlatform : MonoBehaviour
{
    [SerializeField] private float motorSpeed;
    [SerializeField] private Transform topEdge;
    [SerializeField] private Transform bottomEdge;

    private bool playerOnBoard;

    private void Awake()
    {

    }

    private void Update()
    {
        if (playerOnBoard)
            transform.position = new Vector2(transform.position.x, transform.position.y * Time.deltaTime * motorSpeed);
        else
        {
            if(transform.position.y > bottomEdge.transform.position.y)
            {
                transform.position = new Vector2(transform.position.x, transform.position.y * Time.deltaTime * -1 * motorSpeed);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") playerOnBoard = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") playerOnBoard = false;
    }
}