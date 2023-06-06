using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator an;
    public bool isGround;       //���� ������ true;
    public float ObjectImageScale = 1;
    public LayerMask groundLayer;
    public float jumpForce = 0;
    public static float moveSpeed = 3;      //�̵��ӵ� ���⼭ �ٲٸ� ��
    // Start is called before the first frame update
    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(gameObject.tag == "MainPlayer" && Input.GetKey(KeyCode.LeftArrow) || gameObject.tag == "MainPlayer" && Input.GetKey(KeyCode.RightArrow)  //�����÷��̾ ��,��� �����̴� ����Ű�� (��,��)����Ű�̴�
        || gameObject.tag == "SubPlayer" && Input.GetKey(KeyCode.A) || gameObject.tag == "SubPlayer" && Input.GetKey(KeyCode.D))                        //�����÷��̾ ��,��� �����̴� ����Ű�� A,D�̴�
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
            if (isGround)       //���� ��� �ִٸ� �ȴ� ��� ����
            {
                an.SetBool("Run", true);
                
            }
            //�̵��� ���� ĳ���� �̹��� �¿� ����
            if (rb.velocity.x > 0)
            {
                sr.flipX = false;
            }
            if (rb.velocity.x < 0)
            {
                sr.flipX = true;
            }
        }
        if (!isGround)      //���� ��� ���� �ʴٸ� �ȴ� ��� ����
        {
            an.SetBool("Run", false);
            an.SetBool("Jump", true);
        }
        else//���� ��� �ִٸ� ��� ���� ����
        {
            an.SetBool("Jump", false);
        }
        if (gameObject.tag == "MainPlayer" && Input.GetKeyUp(KeyCode.LeftArrow) || gameObject.tag == "MainPlayer" && Input.GetKeyUp(KeyCode.RightArrow) //�����÷��̾ ��,��� �������� ���� �� �ٷ� ���߰� �ϱ�
        || gameObject.tag == "SubPlayer" && Input.GetKeyUp(KeyCode.A) || gameObject.tag == "SubPlayer" && Input.GetKeyUp(KeyCode.D))                    //�����÷��̾ ��,��� �������� ���� �� �ٷ� ���߰� �ϱ�
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            an.SetBool("Run", false);        //�ȴ� ��� ����

        }


        isGround = Physics2D.OverlapCircle(transform.position, ObjectImageScale / 3, groundLayer);
        
        

    }
}
