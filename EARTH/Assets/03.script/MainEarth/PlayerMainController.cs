using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainController : MonoBehaviour
{
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
    
    public float ObjectImageScale = 1;
    public LayerMask groundLayer;
    public float jumpForce = 0;
    public static float moveSpeed = 2;      
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator an;
    public bool isGround;       
    public static bool fadeOut = false;     
    public bool isplayer = false;
    public bool isPlayerOn;
    public Collider2D rayHit;
    public float otherVelocity;
    public LayerMask moveGroundLayer;
    protected GameManager GameManager => GameManager.Instance;


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
        isplayer = Physics2D.OverlapCircle(groundCheck.position, 0.1f, playerLayer);
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        rayHit = Physics2D.OverlapCircle(groundCheck.position, 0.1f, moveGroundLayer);
        isPlayerOn = Physics2D.OverlapCircle(groundCheck.position, 0.2f, playerLayer);
        
        
        if (GameManager.move)
        {
            if (rayHit)
            {
                otherVelocity = rayHit.GetComponent<Rigidbody2D>().velocity.x;
                if (GameManager.joysticDown)
                {
                    rb.velocity = new Vector2(Input.GetAxis(HorizontalKeyMap) * moveSpeed + otherVelocity, rb.velocity.y);
                    if (Input.GetAxis(HorizontalKeyMap) == 0)
                    {
                        rb.velocity = new Vector2(otherVelocity, rb.velocity.y);
                    } 
                }
                else if(GameManager.keyDown)
                {
                    rb.velocity = new Vector2(Input.GetAxis(HorizontalKeyBoard) * moveSpeed + otherVelocity, rb.velocity.y);
                    if(Input.GetAxis(HorizontalKeyBoard) == 0)
                    {
                        rb.velocity = new Vector2(otherVelocity, rb.velocity.y);
                    }
                }
            }
            else
            {
                if (GameManager.keyDown)
                {
                    rb.velocity = new Vector2(Input.GetAxis(HorizontalKeyBoard) * moveSpeed, rb.velocity.y);
                }
                else if (GameManager.joysticDown)
                {
                    rb.velocity = new Vector2(Input.GetAxis(HorizontalKeyMap) * moveSpeed, rb.velocity.y);
                }
            }
        }
        
        if (rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -5, 5));
        }
        if (GameManager.move)
        {
            if (Input.GetAxis(HorizontalKeyMap) == 0 && Input.GetAxis(HorizontalKeyBoard) == 0)
            {
                an.SetBool("Run", false);
            }
            else
            {
                if (Input.GetAxis(HorizontalKeyMap) < 0 || Input.GetAxis(HorizontalKeyBoard) < 0)
                {
                    sr.flipX = true;
                }
                else if (Input.GetAxis(HorizontalKeyMap) > 0 || Input.GetAxis(HorizontalKeyBoard) > 0)
                {
                    sr.flipX = false;
                }
                if (isGround || isPlayerOn)
                {
                    an.SetBool("Run", true);
                }
            }
        }
        
        
    }
}
