using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerAttack))]
public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerAttack PlayerAttack;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        PlayerAttack = GetComponent<PlayerAttack>();
    }

    private void Update()
    {
        //Перемещение персонажа
        float horizontalDirection = Input.GetAxis("Horizontal");
        bool jumpPressed = Input.GetButtonDown("Jump");
        playerMovement.MovePlayer(horizontalDirection, jumpPressed);

        //Стрельба
        if(Input.GetButtonDown("Fire1"))
            PlayerAttack.Attack();
    }
}
