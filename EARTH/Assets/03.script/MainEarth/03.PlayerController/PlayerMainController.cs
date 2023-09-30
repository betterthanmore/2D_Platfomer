using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainController : MonoBehaviour
{
    public enum State
    {
        MOVE,
        IDEL,
        HOLD,
        HEAL,
        SUBHOLD,
        CULTUP,
        CULTUPRUN
    }
    public State state = State.IDEL;

    public enum PLAYERTYPE
    {
        PLAYER_01,
        PLAYER_02
    }
    protected PLAYERTYPE playertype = PLAYERTYPE.PLAYER_01;
    public Transform groundCheck;    
    public LayerMask playerLayer;    
   /* public string ControllerDevices;*/

    protected bool cultup_On = false;
    public CapsuleCollider2D cap_c;
    public bool private_move = true;
    public float size_Init = 0;
    public float size_trans = 0;
    public float offset_init;
    public float offset_trans;

    public int dir = 0;
    public LayerMask groundLayer;
    public float rayRange = 0.1f;
    public float jumpForce = 0;
    public float moveSpeed = 2;
    protected Vector2 playerMoveX;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator an;
    protected bool isStepOn;
    public static bool fadeOut = false;     
    public bool isplayer = false;
    protected Collider2D isPlayerOn;
    protected Collider2D moveGround;
    public RaycastHit2D[] objectSense;
    public float otherVelocity = 0f;
    public LayerMask moveGroundLayer;
    public bool boxHold = false;
    public bool subBoxHold = false;
    public float rayDis = 0.15f;
    public float rayPosy = 0.5f;
    public LayerMask objectDetection;
    public bool boxPush = false;
    /*PlayerInput move_input = new PlayerInput();*/

    protected GameManager GameManager => GameManager.Instance;
    protected UIManger UIManager => UIManger.uiManger;


    // Start is called before the first frame update
    public virtual void Start()
    {
        groundCheck = gameObject.transform.Find("GroundCheckPos");
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
        cap_c = GetComponent<CapsuleCollider2D>();

        /*if (playertype == PLAYERTYPE.PLAYER_02)
        {

            ControllerDevices = "XInputControllerWindows1";
        }
        else if (playertype == PLAYERTYPE.PLAYER_01)
        {
            ControllerDevices = "XInputControllerWindows";
        }*/
    }

    // Update is called once per frame
    public virtual void Update()
    {
        isStepOn = Physics2D.OverlapCircle(groundCheck.position, rayRange, groundLayer);
        isPlayerOn = Physics2D.OverlapCircle(groundCheck.position, 0.2f, playerLayer);
        objectSense = Physics2D.RaycastAll(new Vector3(transform.position.x, transform.position.y  + rayPosy, transform.position.z), Vector2.right * dir, rayDis, objectDetection);
        boxPush = Physics2D.OverlapCapsule(transform.position, cap_c.size * 1.1f, 0, 0, 11);
        if (boxPush)
            Debug.Log("박스 감지");

        if (!private_move)
            playerMoveX.x = 0;

        if (!moveGround && playerMoveX.x == 0)
            rb.velocity = new Vector2(0, rb.velocity.y);

        rb.velocity = new Vector2(playerMoveX.x, rb.velocity.y);
        
    }
    public void Move(InputAction.CallbackContext input)
    {
        /*if (ControllerDevices.Length == 0)
            ControllerDevices = input.control.device.name;*/
        if (/*(input.control.parent.name == ControllerDevices || input.control.device.name == "Keyboard") &&*/ GameManager.move && private_move)
        {
            
            playerMoveX.x = input.ReadValue<Vector2>().x * moveSpeed + otherVelocity;
            if (input.ReadValue<Vector2>().x == 0)
            {
                playerMoveX.x = otherVelocity;
            }
            return;
        }
        else if(/*(input.control.parent.name == ControllerDevices || input.control.device.name == "Keyboard") && */GameManager.move && !private_move)
        {
            playerMoveX.x = 0;
            return;
        }
    }

    public void MoveDirection()
    {
        if (rb.velocity.x < 0)
        {
            sr.flipX = true;
            dir = -1;
        }
        else if (rb.velocity.x > 0)
        {
            sr.flipX = false;
            dir = 1;
        }
    }
    public void NextScene_L_Stick_Press(InputAction.CallbackContext input)
    {
        if (input.started /*&& input.control.device.name == ControllerDevices*/)
        {
            GameManager.nextScene_Press[0] = true;
            if (GameManager.nextScene_Press[0] && GameManager.nextScene_Press[1])
                GameManager.NextScene();
        }
        else if (input.canceled /*&& input.control.device.name == ControllerDevices*/)
        {
            GameManager.nextScene_Press[0] = false;
        }
        else if(input.control.device.name == "Keyboard" && input.started)
            GameManager.NextScene();
    }
    public void NextScene_Window_Press(InputAction.CallbackContext input)
    {
        if (input.started /*&& input.control.device.name == ControllerDevices*/)
        {
            GameManager.nextScene_Press[1] = true;
            if (GameManager.nextScene_Press[0] && GameManager.nextScene_Press[1])
                GameManager.NextScene();
        }
        else if (input.canceled/* && input.control.device.name == ControllerDevices*/)
        {
            GameManager.nextScene_Press[1] = false;
        }
    }
    public void Pause(InputAction.CallbackContext input)        
    {
        if (input.started && !GameManager.buttonB_Lock)
        {
            if (!GameManager.buttonBPress)
            {
                GameManager.buttonBPress = true;
                GameManager.move = false;
                Time.timeScale = 0;
            }
            else
            {
                GameManager.buttonBPress = false;
                GameManager.move = true;
                Time.timeScale = 1;
            } 
        }
    }
    public void AnimationTransform()
    {
        if(state != State.HOLD)
        {
            an.speed = 1;

        }
        switch (state)
        {
            case State.MOVE:
                if ((isStepOn || isPlayerOn))
                {
                    an.SetBool("Run", true);
                    an.SetBool("Jump", false);

                }
                else
                {
                    an.SetBool("Jump", true);
                    an.SetBool("Run", false);
                }
                if (!boxHold)
                {
                    MoveDirection();
                }
                break;
            case State.IDEL:
                an.SetBool("Jump", false);
                an.SetBool("Run", false);
                an.SetBool("Hold", false);
                if(gameObject.layer == 8)
                    an.SetBool("CultUp", false);
                break;
            case State.HOLD:
                an.SetBool("Hold", true);
                moveSpeed = 0.5f;
                if (rb.velocity.x == 0)     
                {
                    an.speed = 0;
                }
                else
                {
                    an.speed = 1;
                }
                break;
            case State.HEAL:
                break;
            case State.SUBHOLD:
                an.SetBool("Hold", true);
                moveSpeed = 0.5f;
                MoveDirection();
                break;
            case State.CULTUP:
                an.SetBool("CultUp", true);
                break;
            case State.CULTUPRUN:
                an.SetBool("Run", true);
                moveSpeed = 0.5f;
                MoveDirection();
                if (rb.velocity.x == 0)
                {
                    an.speed = 0;
                }
                else
                {
                    an.speed = 1;
                }
                break;
            default:
                break;
        }
    }
}
