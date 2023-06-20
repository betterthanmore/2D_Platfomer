using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainKeyBoard : PlayerMainController
{

    protected GameManager GameManager => GameManager.Instance;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(Input.GetAxis("HorizontalMain") * moveSpeed, rb.velocity.y);
            if (rb.velocity.x < 0)                  //�̹��� �¿� ���� �ű�
            {
                sr.flipX = true;
            }
            else if (rb.velocity.x > 0)
            {
                sr.flipX = false;

            }
            if (isGround || isPlayerOn)       //���� ��� �ִٸ� �ȴ� ��� ����
            {
                an.SetBool("Run", true);

            }
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && (isPlayerOn || isGround))
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);
        }
        if (gameObject.tag == "MainPlayer" && Input.GetKeyUp(KeyCode.LeftArrow) || gameObject.tag == "MainPlayer" && Input.GetKeyUp(KeyCode.RightArrow) //�����÷��̾ ��,��� �������� ���� �� �ٷ� ���߰� �ϱ�
        || gameObject.tag == "SubPlayer" && Input.GetKeyUp(KeyCode.A) || gameObject.tag == "SubPlayer" && Input.GetKeyUp(KeyCode.D))                    //�����÷��̾ ��,��� �������� ���� �� �ٷ� ���߰� �ϱ�
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            an.SetBool("Run", false);        //�ȴ� ��� ����

        }
        if (!isGround && isPlayerOn || isGround && !isPlayerOn)      //���� ��� ���� �ʴٸ� �ȴ� ��� ����
        {
            an.SetBool("Jump", false);
        }
        else//���� ��� �ִٸ� ��� ���� ����
        {
            an.SetBool("Run", false);
            an.SetBool("Jump", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)     //��Ϲ����� ���� �ڵ�
    {
        if (other.gameObject.tag == "GearItem")
        {
            Destroy(other.gameObject);
            GameManager.gearItem += 1;
            if(GameManager.gearItem < 5)
            {
                GameManager.mixGears -= 1;
                StartCoroutine(GameManager.MinimumGears());           //6�� 20�� �߰��ϰ� ���� ���̽�ƽ�� ��ũ��Ʈ�� �ȳ��� ������ ���� ��
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BreakAbleObject")
        {
            collision.gameObject.GetComponent<BreakAbleObject>().DoFadeInOut();
        }

    }

}
