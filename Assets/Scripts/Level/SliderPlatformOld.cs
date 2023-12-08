using UnityEngine;

public class SliderPlatformOld : MonoBehaviour
{
    private SliderJoint2D platform;
    private JointMotor2D motor;

    [SerializeField] private float motorSpeed;

    private void Awake()
    {
        platform = GetComponent<SliderJoint2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        motor = platform.motor;
        if(collision.tag == "SliderJointBorder")
        {
            if (collision.name == "BottomEdge" || collision.name == "LeftEdge")
                motor.motorSpeed = motorSpeed * -1;
            else
                motor.motorSpeed = motorSpeed;
        }
        platform.motor = motor;
        
    }

    

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
            collision.transform.parent = null;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
            collision.transform.parent = transform;
    }
}
