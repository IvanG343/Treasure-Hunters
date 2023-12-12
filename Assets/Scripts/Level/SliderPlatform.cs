using UnityEngine;

public class SliderPlatform : MonoBehaviour
{
    [SerializeField] private float motorSpeed;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform endPos;
    private bool movePositive;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "SliderJointBorder")
        {
            if (collision.name == "StartPos")
                movePositive = false;
            else
                movePositive = true;
        }
    }

    private void Update()
    {
        if(movePositive)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos.position, motorSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos.position, motorSpeed * Time.deltaTime);
        }
    }
}
