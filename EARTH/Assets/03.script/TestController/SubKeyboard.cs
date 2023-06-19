using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SubKeyboard : PlayerMainController
{
    public Scrollbar scrollbar;
    public int boostTime = 0;           //�ν�Ʈ �ð�
    public float boostDistanceLimit = 1;//�ν�Ʈ�� �ѹ��� �ö� �� �ִ� �Ÿ�
    public Camera cm;
    private float subplayerPosYCrt;    //�ν�Ʈ ����� ���� �÷��̾��� ���� ��ġ ����
    private float subPlayerPosYTrs;     //�ν�Ʈ ����ϸ鼭 �ٲ�� ��ġY ���� ����
    public bool enableBoost = true;            //�ν�Ʈ ��� ���� ����
    private bool maxDistance = true;            //�ν�Ʈ�� �ö� �Ÿ��� �ִ�ġ�� ���� �� �ڵ����� ��������

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        subPlayerPosYTrs = gameObject.transform.position.y;
        if (Input.GetKeyDown(KeyCode.W) && enableBoost)     //���� ��� �ν�Ʈ Ű�� ������ ���� �÷��̾��� Y�� ���� �����Ѵ�.
        {
            subplayerPosYCrt = gameObject.transform.position.y;
        }
        if (Input.GetKeyUp(KeyCode.W) || !maxDistance)  //����Ű�� ���� �� �ν�Ʈ�� ������ �Ѵ� -> �÷��̾ ���� ���� ������ 
        {
            an.SetBool("Jump", false);
            enableBoost = false;
        }
        if (gameObject.tag == "SubPlayer" && Input.GetKey(KeyCode.W) && scrollbar.size > 0.001 && maxDistance && enableBoost)        //�ν�Ʈ �۵� ��Ű�� �ڵ�
        {
            an.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            scrollbar.size -= 1 * Time.deltaTime / boostTime;
            if (subPlayerPosYTrs >= subplayerPosYCrt + boostDistanceLimit)
            {
                Debug.Log(subplayerPosYCrt);
                maxDistance = false;
            }
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(Input.GetAxis("HorizontalSub") * moveSpeed, rb.velocity.y);
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
        if (isGround)   //���� ��´ٸ�
        {
            enableBoost = true;
            maxDistance = true;
        }
        else if(!isGround && !isPlayerOn)          //���̶� �÷��̾� ��� ���������� ���� ��
        {
            an.SetBool("Run", false);
        }

        if (gameObject.tag == "SubPlayer" && Input.GetKeyUp(KeyCode.A) || gameObject.tag == "SubPlayer" && Input.GetKeyUp(KeyCode.D))                    //�����÷��̾ ��,��� �������� ���� �� �ٷ� ���߰� �ϱ� �̰� ���̽�ƽ�� ��� ��
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            an.SetBool("Run", false);        //�ȴ� ��� ����

        }
        scrollbar.transform.position = cm.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + 1.5f));
    }
}
