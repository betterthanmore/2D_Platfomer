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
            GameManager.gearItem += 1;
        }
    }
    public void Hold(InputAction.CallbackContext input)
    {
        if (boxSense && GameManager.move && (input.control.parent.name == ControllerDevices || input.control.parent.name == "Keyboard"))
        {
            if (rb.velocity.y > -0.1f && rb.velocity.y < 0.1f && input.started)
            {
                state = State.HOLD;
                boxHold = true;
                boxSense.collider.transform.parent = gameObject.transform;
                boxSense.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
            }
            if (input.canceled)
            {
                state = State.IDEL;
                boxHold = false;
                boxSense.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                boxSense.transform.parent = null;
                moveSpeed = 2;
            } 
        }
    }
    public void Heal(InputAction.CallbackContext input)
    {
        if (rb.velocity.y > -0.1f && rb.velocity.y < 0.1f && interaction)
        {
            if (!boxHold && input.started && state != State.HEAL && !boxSense && (input.control.parent.name == ControllerDevices || input.control.parent.name == "Keyboard"))
            {
                if (SubPlayer.scrollbar.size == 1)
                {
                    if (UIManager.minGearText != null && UIManager.minGearTextStart)
                    {
                        StartCoroutine(UIManager.MinimumGears("에너지가 충분하네!"));
                    }
                }
                else if (GameManager.gearItem == 0)
                {
                    if (UIManager.minGearText != null && UIManager.minGearTextStart)
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
        }
    }
    public void Jump(InputAction.CallbackContext input)
    {
        if ((input.control.parent.name == ControllerDevices || input.control.parent.name == "Keyboard") && !boxHold && input.started)
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

        if ((input.control.parent.name == ControllerDevices || input.control.parent.name == "Keyboard") && input.started)
            GameManager.MainLever();
    }
    /*public void HealStop()
    {
        StopCoroutine("SubGaugeHeal");
        state = State.IDEL;
    }*/

}
