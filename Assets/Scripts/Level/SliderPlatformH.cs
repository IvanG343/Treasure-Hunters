using UnityEngine;

public class SliderPlatformH : SliderPlatform
{
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
