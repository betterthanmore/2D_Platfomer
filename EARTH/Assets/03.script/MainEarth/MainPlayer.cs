using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : PlayerMainController
{
    public static int mainDir;
    protected GameManager GameManager => GameManager.Instance;
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
        if (rb.velocity.x > 0)
        {
            mainDir = -1;
        }
        if (rb.velocity.x < 0)
        {
            mainDir = 1;
        }
        if (gameObject.tag == "MainPlayer" && Input.GetButtonDown(JumpKeyMap) && (isGround || isPlayerOn))
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
    private void OnTriggerEnter2D(Collider2D other)     //��Ϲ����� ���� �ڵ�
    {
        if (other.gameObject.tag == "GearItem")
        {
            Destroy(other.gameObject);
            GameManager.gearItem += 1;
            if (GameManager.gearItem < 5)
            {
                GameManager.mixGears -= 1;
                StopCoroutine(GameManager.MinimumGears());
                StartCoroutine(GameManager.MinimumGears());           //6�� 20�� �߰��ϰ� ���� ���̽�ƽ�� ��ũ��Ʈ�� �ȳ��� ������ ���� ��
            }
        }
    }
}
