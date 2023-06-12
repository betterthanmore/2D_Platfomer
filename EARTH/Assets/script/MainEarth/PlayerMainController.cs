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
    public string HorizontalKeyMap = "Horizontal1";
    public string JumpKeyMap = "GamePad1_A";
    public Transform groundCheck;    // 땅 체크를 위한 오브젝트
    public LayerMask playerLayer;    // 플레이어 레이어
    // 아래부턴 내가 한것
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public Animator an;
    public bool isGround;       //땅에 닿으면 true;
    public float ObjectImageScale = 1;
    public LayerMask groundLayer;
    public float jumpForce = 0;
    public static float moveSpeed = 3;      //이동속도 여기서 바꾸면 됭

    // Start is called before the first frame update
    public virtual void Start()
    {
        Debug.Log("베이스 작동");
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
        if (rb.velocity.x != 0)
        {

            if (rb.velocity.x < 0)
            {
                sr.flipX = true;
            }
            else if (rb.velocity.x > 0)
            {
                sr.flipX = false;

            }
            if (isGround)       //땅을 밟고 있다면 걷는 모션 진행
            {
                an.SetBool("Run", true);

            }
        }
        if(rb.velocity.x == 0)
        {
            Debug.Log("반응");
            an.SetBool("Run", false);
        }
        rb.velocity = new Vector2(Input.GetAxis(HorizontalKeyMap) * moveSpeed, rb.velocity.y);


        isGround = Physics2D.OverlapCircle(transform.position, ObjectImageScale / 3, groundLayer);
    }
}
