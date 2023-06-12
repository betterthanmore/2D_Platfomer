using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainController : MonoBehaviour
{
    public enum PLAYERTYPE
    {
        PLAYER_01,
        PLAYER_02
    }
    public PLAYERTYPE playertype = PLAYERTYPE.PLAYER_01;
    public Transform groundCheck;    // �� üũ�� ���� ������Ʈ
    public LayerMask playerLayer;    // �÷��̾� ���̾�
    protected string HorizontalKeyMap = "Horizontal1";
    protected string JumpKeyMap = "GamePad1_A";
    // �Ʒ����� ���� �Ѱ�
    public float ObjectImageScale = 1;
    public LayerMask groundLayer;
    public float jumpForce = 0;
    public static float moveSpeed = 3;      //�̵��ӵ� ���⼭ �ٲٸ� ��
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator an;
    protected bool isGround;       //���� ������ true;


    // Start is called before the first frame update
    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
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

    // Update is called once per frame
    public virtual void Update()
    {

        //�̵��� ���� ĳ���� �̹��� �¿� ����
        if (rb.velocity.x != 0)
        {

            if (rb.velocity.x < 0)
            {
                sr.flipX = true;
            }
            else if (rb.velocity.x > 0)
            {
                sr.flipX = false;

            }
            if (isGround)       //���� ��� �ִٸ� �ȴ� ��� ����
            {
                an.SetBool("Run", true);

            }
        }
        if(rb.velocity.x == 0)
        {
            an.SetBool("Run", false);
        }
        rb.velocity = new Vector2(Input.GetAxis(HorizontalKeyMap) * moveSpeed, rb.velocity.y);


        isGround = Physics2D.OverlapCircle(transform.position, ObjectImageScale / 3, groundLayer);
    }
}
