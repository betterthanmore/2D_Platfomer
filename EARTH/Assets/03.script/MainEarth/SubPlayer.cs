using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubPlayer : PlayerMainController
{
    public static Scrollbar scrollbar;
    public int boostTime = 0;           //�ν�Ʈ �ð�
    public float boostDistanceLimit = 1;//�ν�Ʈ�� �ѹ��� �ö� �� �ִ� �Ÿ�
    public Camera cm;
    private float subplayerPosYCrt;    //�ν�Ʈ ����� ���� �÷��̾��� ���� ��ġ ����
    private float subPlayerPosYTrs;     //�ν�Ʈ ����ϸ鼭 �ٲ�� ��ġY ���� ����
    private bool enableBoost = true;            //�ν�Ʈ ��� ���� ����
    private bool maxDistance = true;            //�ν�Ʈ�� �ö� �Ÿ��� �ִ�ġ�� ���� �� �ڵ����� ��������
    public static int subDir;

    public override void Start()
    {
        base.Start();
        cm = GameObject.Find("Main Camera").GetComponent<Camera>();
        scrollbar = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
        scrollbar.size = GameManager.gauge;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if(rb.velocity.x > 0)
        {
            subDir = -1;
        }
        if(rb.velocity.x < 0)
        {
            subDir = 1;
        }
        subPlayerPosYTrs = gameObject.transform.position.y;
        if (Input.GetButtonDown(JumpKeyMap) && enableBoost)     //���� ��� �ν�Ʈ Ű�� ������ ���� �÷��̾��� Y�� ���� �����Ѵ�.
        {
            subplayerPosYCrt = gameObject.transform.position.y;
        }
        if (Input.GetButtonUp(JumpKeyMap) || !maxDistance)  //����Ű�� 
        {
            an.SetBool("Jump", false);
            enableBoost = false;

        }
        if (gameObject.tag == "SubPlayer" && Input.GetButton(JumpKeyMap) && scrollbar.size > 0.001 && maxDistance && enableBoost)        //�ν�Ʈ
        {
            an.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            scrollbar.size -= 1 * Time.deltaTime /boostTime;
            GameManager.gauge = scrollbar.size;
            if (subPlayerPosYTrs >= subplayerPosYCrt + boostDistanceLimit)
            {
                Debug.Log(subplayerPosYCrt);
                maxDistance = false;
            }
        }
        if (isGround)   //���� ��´ٸ�
        {
            enableBoost = true;
            maxDistance = true;
        }
        else           //������ �������ٸ�
        {
            an.SetBool("Run", false);
        }

        scrollbar.transform.position = cm.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + 0.5f));
    }
}
