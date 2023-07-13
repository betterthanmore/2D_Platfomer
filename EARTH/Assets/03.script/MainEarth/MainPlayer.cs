using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : PlayerMainController
{
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

        if (!GameManager.butttonBPress)
        {
            if (gameObject.tag == "MainPlayer" && Input.GetButtonDown(JumpKeyMap) && (isGround || isPlayerOn))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            } 
        }
        if (isPlayerOn || isGround)      //���� ��� ���� �ʴٸ� �ȴ� ��� ����
        {
            an.SetBool("Jump", false);
        }
        else
        {
            Debug.Log("���� �÷��̾� �޸��� ��� ����");
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
            if (GameManager.remainGears >= 0)
            {
                GameManager.remainGears -= 1;
                StopCoroutine(GameManager.MinimumGears());
                StartCoroutine(GameManager.MinimumGears());           //6�� 20�� �߰��ϰ� ���� ���̽�ƽ�� ��ũ��Ʈ�� �ȳ��� ������ ���� ��
            }
        }
    }
}
