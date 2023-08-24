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
        if (!boxHold)
        {
            if (rb.velocity.x == 0 && (isStepOn || isPlayerOn))
            {
                state = State.IDEL;
            }
            else
            {
                state = State.MOVE;
            }
        }

        interaction = Physics2D.Raycast(transform.position + new Vector3(0, 0.5f, 0), Vector2.right * dir, 0.5f, playerLayer);
        if (GameManager.move)
        {
            if (boxSense)
            {
                HoldAction();
            }

            if (!boxHold)
            {
                if (Input.GetButtonDown(JumpKeyMap) && (isStepOn || isPlayerOn) || Input.GetKeyDown(KeyCode.W) && (isStepOn || isPlayerOn))
                {
                    JumpAction();
                }

                if (rb.velocity.y > -0.1f && rb.velocity.y < 0.1f)
                {
                    if (interaction && (Input.GetButtonDown("GamePad1_X") || Input.GetKeyDown(KeyCode.H)) && state != State.HEAL && !boxSense)// && (!leverPos1 || 1leverPos2))
                    {
                        an.SetTrigger("Heal");
                        state = State.HEAL;
                        StartCoroutine(SubGaugeHeal());
                    }  
                }
            }
        }
        /*if (state != State.HEAL)
        {
            if (isPlayerOn || isStepOn)
            {
                state = State.IDEL;
            }

            else
            {
                an.SetBool("Run", false);
                state = State.IDEL;
            }

        }*/
        AnimationTransform();

    }
    IEnumerator SubGaugeHeal()
    {
        GameManager.move = false;
        yield return null;
        yield return new WaitForSeconds(an.GetCurrentAnimatorStateInfo(0).length);
        an.SetTrigger("Escape");
        state = State.IDEL;
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
    public void JumpAction()
    {
        state = State.MOVE;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }

    public void HealStop()
    {
        StopCoroutine("SubGaugeHeal");
        state = State.IDEL;
    }

    public void HoldAction()
    {
        if (rb.velocity.y > - 0.1f && rb.velocity.y < 0.1f)
        {
            if (Input.GetButtonDown("GamePad1_X") || Input.GetKeyDown(KeyCode.H))
            {
                Debug.Log("��ư ����");
                state = State.HOLD;
                boxHold = true;
                boxSense.collider.transform.parent = gameObject.transform;
                boxSense.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }

            if (Input.GetButtonUp("GamePad1_X") || Input.GetKeyUp(KeyCode.H))
            {
                state = State.IDEL;
                boxHold = false;
                boxSense.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                boxSense.transform.parent = null;
                moveSpeed = 2;
            } 
        }
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
