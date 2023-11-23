using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Transform followingTarget;
    [SerializeField, Range(0f, 1f)] float parallaxStrngth;
    [SerializeField] bool disableVerticalParallax;

    Vector3 targetPreviousPostition;

    private void Start()
    {
        if(!followingTarget)
            followingTarget = Camera.main.transform;

        targetPreviousPostition = followingTarget.position;
    }

    private void Update()
    {
        Vector3 delta = followingTarget.position - targetPreviousPostition;

        if (disableVerticalParallax)
            delta.y = 0;

        targetPreviousPostition = followingTarget.position;
        transform.position += delta * parallaxStrngth;
    }
}
