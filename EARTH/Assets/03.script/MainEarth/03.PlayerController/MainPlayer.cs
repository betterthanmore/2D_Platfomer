using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

        if (!boxHold && !cultup_On)
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


        if (GameManager.move)
        {
            AnimationTransform();
        }
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
        float maxGauge = 1;       //최대 게이지량에서
        float needGauge = 0;       //필요 게이지량만큼 더해주기 위해
        int needGears = 0;


        needGauge = maxGauge - SubPlayer.scrollbar.size;

        needGears = (int)(Mathf.Ceil(needGauge / 0.05f));
        if (needGears <= GameManager.gearItem)
        {
            UIManager.GearNumText(GameManager.gearItem -= needGears);
            SubPlayer.scrollbar.size += needGears * 0.05f;
        }
        else
        {
            needGears = GameManager.gearItem;
            GameManager.gearItem = 0;
            UIManager.GearNumText(0);
            SubPlayer.scrollbar.size += needGears * 0.05f;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)     //톱니바퀴에 관한 코드
    {
        if (other.gameObject.tag == "GearItem")
        {
            Destroy(other.gameObject);
            GameManager.GearNumText(1);
        }
    }
    public void CultUp(InputAction.CallbackContext input)
    {
        if (!private_move)
            return;
        else if(objectSense.Length == 0 && rb.velocity.y > -0.1f && rb.velocity.y < 0.1f && input.performed && GameManager.move)
        {
            if (!cultup_On)
            {
                cultup_On = true;
                private_move = false;
                state = State.CULTUP;
                size_Init = cap_c.size.y;
                offset_init = cap_c.offset.y;
                StartCoroutine(CultUP_On()); 
            }
            else
            {
                private_move = false;
                cap_c.size = new Vector2(cap_c.size.x, size_Init);
                cap_c.offset = new Vector2(0, offset_init);
                state = State.IDEL;
                StartCoroutine(CultUP_On_Back());
                
            }
        }
    }
    IEnumerator CultUP_On_Back()
    {
        yield return new WaitForSeconds(an.GetCurrentAnimatorClipInfo(0).Length);
        cultup_On = false;
        moveSpeed = 2;
        private_move = true;
    }
    IEnumerator CultUP_On()
    {
        cap_c.offset = new Vector2(0, offset_trans);
        cap_c.size = new Vector2(cap_c.size.x, size_trans);
        yield return new WaitForSeconds(an.GetCurrentAnimatorClipInfo(0).Length);
        state = State.CULTUPRUN;
        private_move = true;
    }
    public void HealandBoxHold(InputAction.CallbackContext input)
    {
        Debug.Log("힐 버튼");
        if (rb.velocity.y > -0.1f && rb.velocity.y < 0.1f && GameManager.move)
        {
            if (objectSense.Length == 0)
            {
                Debug.Log("리턴");
                return;
            }
            else if (input.started)
            {
                foreach (var item in objectSense)
                {
                    if (item.collider.gameObject.layer == 11 && state != State.HOLD)
                    {
                        Debug.Log("잡기");
                        state = State.HOLD;
                        boxHold = true;
                        item.collider.transform.parent = gameObject.transform;
                        item.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

                    }
                    else if (state == State.HOLD)
                    {
                        Debug.Log("놓기");
                        state = State.IDEL;
                        boxHold = false;
                        item.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                        item.transform.parent = null;
                        moveSpeed = 2;
                    }
                    else if (item.collider.gameObject.layer == 9 && state != State.HOLD && state != State.HEAL)
                    {
                        Debug.Log("수리");
                        if (SubPlayer.scrollbar.size == 1)
                        {
                            if (UIManager.minGearText != null)
                            {
                                StartCoroutine(UIManager.MinimumGears("에너지가 충분하네!"));
                            }
                        }
                        else if (GameManager.gearItem == 0)
                        {
                            if (UIManager.minGearText != null)
                            {
                                StartCoroutine(UIManager.MinimumGears("기어 아이템이 없네.."));
                            }
                        }
                        else
                        {
                            an.SetTrigger("Heal");
                            state = State.HEAL;
                            StartCoroutine(SubGaugeHeal());
                        }
                    }
                    break;
                }
            }
        }
    }
    public void ReLoad(InputAction.CallbackContext input)
    {
        if (input.started && !GameManager.buttonB_Lock && GameManager.reGameButtonDown)
        {
            GameManager.reGame1P = true;
            if (GameManager.reGame1P && GameManager.reGame2P)
            {
                GameManager.reGameButtonDown = false;
                GameManager.ReGameStart();
            }
        }

    }
    public void Portar(InputAction.CallbackContext input)
    {
        for (int i = 0; i < GameManager.portalOnPlayer.Length; i++)
        {
            if (input.started && GameManager.portalOnPlayer[i].gameObject.tag == "MainPlayer")
            {
                Debug.Log("메인 포탈");
                if (GameManager.portal_Ready_Player[0] && !GameManager.portal_Ready_Player[1])
                {
                    StartCoroutine(UIManager.MinimumGears("로봇이 준비 될 때까지 기다려주자"));
                }
                else
                {
                    GameManager.MainPortalAction();
                }
            } 
        }
    }
    public void Jump(InputAction.CallbackContext input)
    {
        if (/*(input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard") && */!boxHold && input.started && !cultup_On)
        {
            if (isStepOn || isPlayerOn)
            {
                state = State.MOVE;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
            return;
        }
    }
    public void Lever(InputAction.CallbackContext input)
    {
        Debug.Log("반응");
        Debug.Log(input.control.device.name);
        Debug.Log(input.interaction);
        if (/*(input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard") &&*/ input.started)
            GameManager.MainLever();
    }
}
