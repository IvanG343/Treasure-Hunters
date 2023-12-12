using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    private Transform parentPlatform;

    private void Awake()
    {
        parentPlatform = transform.parent.Find("Platform").gameObject.GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position = parentPlatform.position;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.transform.parent = null;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.transform.parent = transform;
    }
}
