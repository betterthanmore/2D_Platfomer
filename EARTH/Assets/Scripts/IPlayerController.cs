using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayerController : MonoBehaviour
{
    public enum PLAYERTYPE
    {
        PLAYER_01,
        PLAYER_02
    }

    public float speed = 5f;         // �÷��̾� �̵� �ӵ�
    public float jumpForce = 10;     // ���� ��
    public Transform groundCheck;    // �� üũ�� ���� ������Ʈ
    public LayerMask groundLayer;    // ���� ��Ÿ���� ���̾�
    public LayerMask playerLayer;    // �÷��̾� ���̾�

    public Rigidbody2D rb;          // Rigidbody2D ������Ʈ ����

    public bool isGrounded;         // ���� ��Ҵ��� ����
    public bool isPlayerOn;         // �÷��̾� ���� �ִ��� üũ

    public string HorizontalKeyMap = "Horizontal1";
    public string JumpKeyMap = "GamePad1_A";

    public PLAYERTYPE playertype = PLAYERTYPE.PLAYER_01;

    public float moveHorizontal = 0f;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D ������Ʈ �Ҵ�

        if (playertype == PLAYERTYPE.PLAYER_01)
        {
            HorizontalKeyMap = "Horizontal1";
            JumpKeyMap = "GamePad1_A";
        }
        else if (playertype == PLAYERTYPE.PLAYER_02)
        {
            HorizontalKeyMap = "Horizontal2";
            JumpKeyMap = "GamePad2_A";
        }
    }

    public virtual void Update()
    {
        // ���� �̵� �Է� ��
        moveHorizontal = Input.GetAxis(HorizontalKeyMap);

        // �� üũ
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);


        // �÷��̾� �浹 üũ
        Collider2D[] colliders = new Collider2D[10]; // ����� ������ �迭 ����
        int colliderCount = Physics2D.OverlapCircleNonAlloc(groundCheck.position, 0.2f, colliders, playerLayer);
        isPlayerOn = false;

        for (int i = 0; i < colliderCount; i++)
        {
            if (colliders[i].gameObject != gameObject) // �ڱ� �ڽ��� �ƴ� ���
            {
                isPlayerOn = true;
                break;
            }
        }

        // ���� �Է� ó��
        if (Input.GetButtonDown(JumpKeyMap) && (isGrounded || isPlayerOn))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        if(transform.position.y < -30.0f)   //�Ʒ��� �������� �ı�
        {
            Destroy(gameObject);
        }
    }

    public virtual void FixedUpdate()
    {
        // �߷� ����
        rb.AddForce(new Vector2(0f, -9.8f * rb.mass));
    }
}



