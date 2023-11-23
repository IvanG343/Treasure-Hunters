using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header ("Patrol Edges")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header ("Enemy Transform")]
    [SerializeField] private Transform enemy;

    [Header("Enemy Animator")]
    [SerializeField] private Animator animator;

    [Header ("Movement params")]
    [SerializeField] private float speed;
    private Vector2 initScale;
    private bool movingLeft;

    [Header("Idle Beahaviour")]
    [SerializeField] private float idleDuration;
    private float idleTimer;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        animator.SetBool("isMoving", false);
    }

    private void Update()
    {
        if(movingLeft)
        {
            if(enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
            
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        animator.SetBool("isMoving", false);

        idleTimer += Time.deltaTime;
        if(idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        //Анимация
        animator.SetBool("isMoving", true);
        //Флип спрайта
        enemy.localScale = new Vector2(Mathf.Abs(initScale.x) * _direction, initScale.y);
        //Движение
        enemy.position = new Vector2(enemy.position.x + Time.deltaTime * _direction * speed, enemy.position.y);
    }
}
