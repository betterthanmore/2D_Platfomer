using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : IPlayerController
{
    // Start is called before the first frame update
    public override void Start()        //�÷��̾� �̵�
    {
        base.Start();       //�θ� ��ũ��Ʈ�� start�޼����� ������ ������
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

       

        if ((Input.GetKey(KeyCode.A) && playertype == PLAYERTYPE.PLAYER_01)
            || (Input.GetKey(KeyCode.LeftArrow) && playertype == PLAYERTYPE.PLAYER_02))
        {
            moveHorizontal = -1f;
        }
        else if ((Input.GetKey(KeyCode.D) && playertype == PLAYERTYPE.PLAYER_01) 
            || (Input.GetKey(KeyCode.RightArrow) && playertype == PLAYERTYPE.PLAYER_02))
        {
            moveHorizontal = 1f;
        }

    

        // ���� �Է� ó��
        if ((Input.GetKeyDown(KeyCode.W) && isGrounded && playertype == PLAYERTYPE.PLAYER_01) ||
            (Input.GetKeyDown(KeyCode.P) && isGrounded && playertype == PLAYERTYPE.PLAYER_02) ||
            (Input.GetKeyDown(KeyCode.W) && isPlayerOn && playertype == PLAYERTYPE.PLAYER_01) ||
            (Input.GetKeyDown(KeyCode.P) && isPlayerOn && playertype == PLAYERTYPE.PLAYER_02))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
    public override void FixedUpdate()
    {
        Vector2 movement = new Vector2(moveHorizontal, 0f);

        // �� �浹 üũ
        bool isCollidingWithWall = Physics2D.Raycast(transform.position, movement, 0.1f, groundLayer);

        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

        base.FixedUpdate();
    }
}
