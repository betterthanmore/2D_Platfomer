using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubPlayer : PlayerMainController
{
    public Scrollbar scrollbar;
    public int boostTime = 0;           //�ν�Ʈ �ð�
    public float boostDistanceLimit = 1;//�ν�Ʈ�� �ѹ��� �ö� �� �ִ� �Ÿ�
    public Camera cm;
    private float subplayerPosYCrt;    //�ν�Ʈ ����� ���� �÷��̾��� ���� ��ġ ����
    private float subPlayerPosYTrs;     //�ν�Ʈ ����ϸ鼭 �ٲ�� ��ġY ���� ����
    private bool enableBoost = true;            //�ν�Ʈ ��� ���� ����
    private bool maxDistance = true;            //�ν�Ʈ�� �ö� �Ÿ��� �ִ�ġ�� ���� �� �ڵ����� ��������

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
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

        scrollbar.transform.position = cm.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + 1.5f));
    }
}