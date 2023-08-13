using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : PlayerMainController
{
    
    public bool interaction;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        interaction = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector2.right * dir, 0.5f, playerLayer);
        
        if (GameManager.move)
        {
            if (gameObject.tag == "MainPlayer" && Input.GetButtonDown(JumpKeyMap) && (isGround || isPlayerOn) || Input.GetKeyDown(KeyCode.W) && (isGround || isPlayerOn))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }

            if (interaction && (Input.GetButtonDown("GamePad1_X") || Input.GetKeyDown(KeyCode.H)) && state != State.HEAL)
            {
                state = State.HEAL;
               
                StartCoroutine(SubGaugeHeal());
            }
        }
        if (state != State.HEAL)
        {
            if (isPlayerOn || isGround)      //���� ��� ���� �ʴٸ� �ȴ� ��� ����
            {
                an.SetBool("Jump", false);
            }
            else
            {
                an.SetBool("Run", false);
                an.SetBool("Jump", true);
            } 

        }
    }
    IEnumerator SubGaugeHeal()
    {
        an.SetTrigger("Heal");
        GameManager.move = false;
        yield return null;
        yield return new WaitForSeconds(an.GetCurrentAnimatorStateInfo(0).length);
        state = State.IDEL;
        an.SetTrigger("Escape");
        GameManager.move = true;
        Heal();

    }

    public void Heal()
    {
       
        if (GameManager.gearItem == 0)
        {
            if( UIManager.minGearText != null)
            {
                UIManager.minGearText.text = "��� �������� ����..";
                StartCoroutine(UIManager.MinimumGears());
                Debug.Log("�� ����");
                state = State.IDEL;
            }
            return;
        }
        float maxGauge = 1;       //�ִ� ������������
        float needGauge = 0;       //�ʿ� ����������ŭ �����ֱ� ����
        int needGears = 0;
        
        
        needGauge = maxGauge - SubPlayer.scrollbar.size;
        
        needGears = (int)(needGauge / 0.05f);
        if(needGears <= GameManager.gearItem)
        {
            GameManager.gearItem -= needGears;
            SubPlayer.scrollbar.size += needGears * 0.05f;
        }
        else
        {
            needGears = GameManager.gearItem;
            GameManager.gearItem = 0;
            SubPlayer.scrollbar.size += needGears * 0.05f;
        }
        
    }
    public void HealStop()
    {
        StopCoroutine("SubGaugeHeal");
        state = State.IDEL;
    }

    private void OnTriggerEnter2D(Collider2D other)     //��Ϲ����� ���� �ڵ�
    {
        if (other.gameObject.tag == "GearItem")
        {
            Destroy(other.gameObject);
            GameManager.gearItem += 1;
            /*if (GameManager.remainGears > 0)
            {
                GameManager.remainGears -= 1;
                StopCoroutine(UIManager.MinimumGears());
                StartCoroutine(UIManager.MinimumGears());           //6�� 20�� �߰��ϰ� ���� ���̽�ƽ�� ��ũ��Ʈ�� �ȳ��� ������ ���� ��
            }*/
        }
    }
}
