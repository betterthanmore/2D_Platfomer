using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainKeyBoard : PlayerMainController
{
    public bool isPlayerOn;
    public static int sawtoothWheelNum = 0;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
        isPlayerOn = Physics2D.OverlapCircle(groundCheck.position, 0.2f, playerLayer);  //�÷��̾� �Ӹ��� ��� �ִٸ�

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(Input.GetAxis("HorizontalMain") * moveSpeed, rb.velocity.y);
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
        if(other.gameObject.tag == "SawtoothWheel")
        {
            Destroy(other);
            sawtoothWheelNum++;
        }
    }

}
