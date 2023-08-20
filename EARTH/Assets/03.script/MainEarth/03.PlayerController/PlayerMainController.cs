using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainController : MonoBehaviour
{
    public enum State
    {
        MOVE,
        IDEL,
        HOLD,
        HEAL
    }
    public State state = State.IDEL;

    public enum PLAYERTYPE
    {
        PLAYER_01,
        PLAYER_02
    }
    public PLAYERTYPE playertype = PLAYERTYPE.PLAYER_01;
    public Transform groundCheck;    
    public LayerMask playerLayer;    
    protected string HorizontalKeyMap = "Horizontal1";
    protected string HorizontalKeyBoard;
    protected string JumpKeyMap = "GamePad1_A";

    public int dir = 0;
    public LayerMask groundLayer;
    public float rayRange = 0.1f;
    public float jumpForce = 0;
    public float moveSpeed = 2;      
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator an;
    public bool isStepOn;
    public static bool fadeOut = false;     
    public bool isplayer = false;
    public Collider2D isPlayerOn;
    public Collider2D moveGround;
    public RaycastHit2D boxSense;
    public float otherVelocity;
    public LayerMask moveGroundLayer;
    public bool boxHold = false;
    protected GameManager GameManager => GameManager.Instance;
    protected UIManger UIManager => UIManger.uiManger;


    // Start is called before the first frame update
    public virtual void Start()
    {
        groundCheck = gameObject.transform.Find("GroundCheckPos");
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();

        if (playertype == PLAYERTYPE.PLAYER_01)
        {
            HorizontalKeyMap = "Horizontal1";
            JumpKeyMap = "GamePad1_A";
        }
        else if (playertype == PLAYERTYPE.PLAYER_02)
        {
            HorizontalKeyMap = "Horizontal2";
            JumpKeyMap = "GamePad2_A";
        }
        if(gameObject.tag == "MainPlayer")
        {
            HorizontalKeyBoard = "HorizontalMain";
        }
        else if(gameObject.tag == "SubPlayer")
        {
            HorizontalKeyBoard = "HorizontalSub";
        }
    }

    // Update is called once per frame
    public virtual void Update()
    {
        isStepOn = Physics2D.OverlapCircle(groundCheck.position, rayRange, groundLayer);
        moveGround = Physics2D.OverlapCircle(groundCheck.position, rayRange, moveGroundLayer);
        isPlayerOn = Physics2D.OverlapCircle(groundCheck.position, 0.2f, playerLayer);
        boxSense = Physics2D.Raycast(new Vector3(transform.position.x, transform.position.y  + 0.5f, transform.position.z), Vector2.right * dir, 0.15f, 2048);

        if (GameManager.move)
        {
            if (moveGround)
            {
                otherVelocity = moveGround.GetComponent<Rigidbody2D>().velocity.x;
                if (GameManager.joysticDown)
                {
                    rb.velocity = new Vector2(Input.GetAxis(HorizontalKeyMap) * moveSpeed + otherVelocity, rb.velocity.y);
                    /*rb.velocity = new Vector2(Input.GetAxis(HorizontalKeyMap) * moveSpeed, rb.velocity.y);*/
                    if (Input.GetAxis(HorizontalKeyMap) == 0)
                    {
                        rb.velocity = new Vector2(otherVelocity, rb.velocity.y);
                    }
                }
                else if (GameManager.keyDown)
                {
                    rb.velocity = new Vector2(Input.GetAxisRaw(HorizontalKeyBoard) * moveSpeed + otherVelocity, rb.velocity.y);
                    /*rb.velocity = new Vector2(Input.GetAxis(HorizontalKeyBoard) * moveSpeed, rb.velocity.y);*/
                    if (Input.GetAxis(HorizontalKeyBoard) == 0)
                    {
                        rb.velocity = new Vector2(otherVelocity, rb.velocity.y);
                    }
                }
            }
            else
            {
                if (GameManager.keyDown)
                {
                    if (boxHold)
                    {
                        if(dir == -1)
                        {
                            rb.velocity = new Vector2(Mathf.Clamp(Input.GetAxisRaw(HorizontalKeyBoard) * moveSpeed, 0, Input.GetAxisRaw(HorizontalKeyBoard) * moveSpeed), rb.velocity.y);
                        }
                        else if(dir == 1)
                        {
                            rb.velocity = new Vector2(Mathf.Clamp(Input.GetAxisRaw(HorizontalKeyBoard) * moveSpeed, Input.GetAxisRaw(HorizontalKeyBoard) * moveSpeed, 0), rb.velocity.y);
                        }
                    }
                    else
                    {
                        rb.velocity = new Vector2(Input.GetAxisRaw(HorizontalKeyBoard) * moveSpeed, rb.velocity.y);

                    }
                }
                else if (GameManager.joysticDown)
                {
                    if (boxHold)
                    {
                        if (dir == -1)
                        {
                            rb.velocity = new Vector2(Mathf.Clamp(Input.GetAxisRaw(HorizontalKeyMap) * moveSpeed, 0, Input.GetAxisRaw(HorizontalKeyMap) * moveSpeed), rb.velocity.y);
                        }
                        else if(dir == 1)
                        {
                            rb.velocity = new Vector2(Mathf.Clamp(Input.GetAxisRaw(HorizontalKeyMap) * moveSpeed, Input.GetAxisRaw(HorizontalKeyMap) * moveSpeed, 0), rb.velocity.y);
                        }
                    }
                    else
                    {
                        rb.velocity = new Vector2(Input.GetAxis(HorizontalKeyMap) * moveSpeed, rb.velocity.y);

                    }
                }
            }

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
                    if (Input.GetAxis(HorizontalKeyMap) < 0 || Input.GetAxisRaw(HorizontalKeyBoard) < 0)
                    {
                        sr.flipX = true;
                        dir = -1;
                    }
                    else if (Input.GetAxis(HorizontalKeyMap) > 0 || Input.GetAxisRaw(HorizontalKeyBoard) > 0)
                    {
                        sr.flipX = false;
                        dir = 1;
                    } 
                }
                break;
            case State.IDEL:
                an.SetBool("Jump", false);
                an.SetBool("Run", false);
                an.SetBool("Hold", false);
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
            default:
                break;
        }
    }
}
