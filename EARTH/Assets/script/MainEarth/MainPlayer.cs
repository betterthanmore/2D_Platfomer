using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : PlayerMainController
{
   
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (gameObject.tag == "MainPlayer" && Input.GetButtonDown(JumpKeyMap) && (isGround))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
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
    }
}
