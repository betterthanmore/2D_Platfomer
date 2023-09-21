using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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

        needGears = (int)(needGauge / 0.05f);
        if (needGears <= GameManager.gearItem)
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
        if(!objectSense && rb.velocity.y > -0.1f && rb.velocity.y < 0.1f && input.started && GameManager.move 
            && (input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard"))
        {
            cultup_On = true;
            private_move = false;
            size_Init[0] = cap_c.size.x;
            size_Init[1] = cap_c.size.y;
            StartCoroutine(CultUP_On());
            

        }
        else if(!cultup_On && GameManager.move && input.canceled && private_move
            && (input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard"))
        {
            //여기에 다시 일어서는 로직을 자면 됨
        }
    }
    IEnumerator CultUP_On()
    {
        cap_c.offset = new Vector2(0, 0.2207956f);
        cap_c.size = new Vector2(size_Init[0], size_Init[1]);
        yield return new WaitForSeconds(an.GetCurrentAnimatorStateInfo(0).length);
        private_move = true;
    }
    public void HealandBoxHold(InputAction.CallbackContext input)
    {
        if (rb.velocity.y > -0.1f && rb.velocity.y < 0.1f && input.started && GameManager.move && (input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard"))
        {
            if (objectSense.collider == null)
                return;
            else if (objectSense.collider.gameObject.layer == 2048)
            {
                state = State.HOLD;
                boxHold = true;
                objectSense.collider.transform.parent = gameObject.transform;
                objectSense.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
            else if (state != State.HEAL && interaction)
            {
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
            else
            {
                Debug.Log("최종");
            }
        }
        else if(rb.velocity.y > -0.1f && rb.velocity.y < 0.1f && boxHold &&  input.canceled && GameManager.move && (input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard"))
        {
            state = State.IDEL;
            boxHold = false;
            objectSense.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            objectSense.transform.parent = null;
            moveSpeed = 2;
        }
    }
    public void ReLoad(InputAction.CallbackContext input)
    {
        if (input.started && !GameManager.buttonB_Lock && GameManager.reGameButtonDown && (input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard"))
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
        if (GameManager.portalOnPlayer == null)
            return;
        else if ((GameManager.portalOnPlayer && input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard") && input.started)
        {
            if (GameManager.portal_Ready_Player[0])
            {
                StartCoroutine(UIManager.MinimumGears("로봇이 준비 될 때까지 기다려주자"));
            }
            else
                GameManager.PortalAction();
        }
    }
    public void Jump(InputAction.CallbackContext input)
    {
        if ((input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard") && !boxHold && input.started && !cultup_On)
        {
            if (isStepOn || isPlayerOn)
            {
                state = State.MOVE;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
    public void Lever(InputAction.CallbackContext input)
    {
        Debug.Log("반응");

        if ((input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard") && input.started)
            GameManager.MainLever();
    }
}
