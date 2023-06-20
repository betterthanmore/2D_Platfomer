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
    public Transform groundCheck;    // 땅 체크를 위한 오브젝트
    public LayerMask playerLayer;    // 플레이어 레이어
    protected string HorizontalKeyMap = "Horizontal1";
    protected string JumpKeyMap = "GamePad1_A";
    // 아래부턴 내가 한것
    public float ObjectImageScale = 1;
    public LayerMask groundLayer;
    public float jumpForce = 0;
    public static float moveSpeed = 2;      //이동속도 여기서 바꾸면 됭
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;
    protected Animator an;
    protected bool isGround;       //땅에 닿으면 true;
    public static bool fadeOut = false;     //화면 전환 불값으로 페이드 아웃 실행여부
    public bool isplayer = false;
    public bool isPlayerOn;

    // Start is called before the first frame update
    public virtual void Start()
    {
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
    }

    // Update is called once per frame
    public virtual void Update()
    {
         
        //이동에 따른 캐릭터 이미지 좌우 반전
        isPlayerOn = Physics2D.OverlapCircle(groundCheck.position, 0.2f, playerLayer);  //플레이어 머리를 밟고 있다면

        if (rb.velocity.x == 0)
        {
            an.SetBool("Run", false);
        }
        rb.velocity = new Vector2(Input.GetAxis(HorizontalKeyMap) * moveSpeed, rb.velocity.y);
        if(rb.velocity.y < 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -5, 5));
        }
        if (Input.GetAxis(HorizontalKeyMap) != 0)
        {
            if (rb.velocity.x < 0)
            {
                sr.flipX = true;
            }
            else if (rb.velocity.x > 0)
            {
                sr.flipX = false;

            }
            if (isGround || isPlayerOn)       //땅을 밟고 있다면 걷는 모션 진행
            {
                an.SetBool("Run", true);

            }
        }

        isplayer = Physics2D.OverlapCircle(groundCheck.position, 0.2f, playerLayer);
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
}
