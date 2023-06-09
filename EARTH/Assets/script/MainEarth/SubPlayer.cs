using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubPlayer : PlayerMainController
{
    public float boostDistanceLimit = 1;
    public Scrollbar scr;
    public int boostTime = 0;           //�ν�Ʈ �ð�
    public float boostDistance = 0;     //�ν�Ʈ�� �ѹ��� �ö� �� �ִ� �Ÿ�
    private float subplayerPosY;    //�ν�Ʈ ����� ���� �÷��̾��� ��ġ ����
    private bool enableBoost = true;            //�ν�Ʈ ��� ���� ����
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.W) && enableBoost)     //���� ��� �ν�Ʈ Ű�� ������ ���� �÷��̾��� Y�� ���� �����Ѵ�.
        {
            subplayerPosY = gameObject.transform.position.y;
        }
        if (gameObject.tag == "SubPlayer" && Input.GetKey(KeyCode.W) && rb.velocity.y <= boostDistanceLimit && scr.value > 0.001 
            && enableBoost /*���⿡ �÷��̾ �ö� �� �ִ� �Ÿ��� ���� �α�*/)        //�ν�Ʈ
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            scr.value -= 1 * Time.fixedDeltaTime /boostTime;
        }
        if (isGround)   //���� ��´ٸ�
        {
            enableBoost = true;
        }
        else           //������ �������ٸ�
        {
            enableBoost = false;
        }
        
    }
}
