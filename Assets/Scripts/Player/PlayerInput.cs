using UnityEngine;

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
        //����������� ���������
        float horizontalDirection = Input.GetAxis("Horizontal");
        bool jumpPressed = Input.GetButtonDown("Jump");
        playerMovement.MovePlayer(horizontalDirection, jumpPressed);

        //�����
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.RightControl))
            PlayerAttack.Attack();

        //��������
        //if (Input.GetButtonDown("Fire2"))
        //    PlayerAttack.RangeAttack();
    }
}
