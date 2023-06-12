using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : PlayerMainController
{
    public bool isPlayerOn;
    // Start is called before the first frame update
    public override void Start()
    {
        Debug.Log("�۵�");
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        isPlayerOn = Physics2D.OverlapCircle(groundCheck.position, 0.2f, playerLayer);

        if (gameObject.tag == "MainPlayer" && Input.GetButtonDown(JumpKeyMap) && (isGround))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
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
}
