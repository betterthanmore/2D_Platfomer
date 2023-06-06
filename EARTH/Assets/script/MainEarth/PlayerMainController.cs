using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMainController : MonoBehaviour
{
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
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if(gameObject.tag == "MainPlayer" && Input.GetKey(KeyCode.LeftArrow) || gameObject.tag == "MainPlayer" && Input.GetKey(KeyCode.RightArrow)  //메인플레이어가 좌,우로 움직이는 방향키는 (좌,우)방향키이다
        || gameObject.tag == "SubPlayer" && Input.GetKey(KeyCode.A) || gameObject.tag == "SubPlayer" && Input.GetKey(KeyCode.D))                        //서브플레이어가 좌,우로 움직이는 방향키는 A,D이다
        {
            rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
            if (isGround)       //땅을 밟고 있다면 걷는 모션 진행
            {
                an.SetBool("Run", true);
                
            }
            //이동에 따른 캐릭터 이미지 좌우 반전
            if (rb.velocity.x > 0)
            {
                sr.flipX = false;
            }
            if (rb.velocity.x < 0)
            {
                sr.flipX = true;
            }
        }
        if (!isGround)      //땅을 밟고 있지 않다면 걷는 모션 중지
        {
            an.SetBool("Run", false);
            an.SetBool("Jump", true);
        }
        else//땅을 밟고 있다면 모션 점프 중지
        {
            an.SetBool("Jump", false);
        }
        if (gameObject.tag == "MainPlayer" && Input.GetKeyUp(KeyCode.LeftArrow) || gameObject.tag == "MainPlayer" && Input.GetKeyUp(KeyCode.RightArrow) //메인플레이어가 좌,우로 움직임을 멈출 때 바로 멈추게 하기
        || gameObject.tag == "SubPlayer" && Input.GetKeyUp(KeyCode.A) || gameObject.tag == "SubPlayer" && Input.GetKeyUp(KeyCode.D))                    //서브플레이어가 좌,우로 움직임을 멈출 때 바로 멈추게 하기
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            an.SetBool("Run", false);        //걷는 모션 중지

        }


        isGround = Physics2D.OverlapCircle(transform.position, ObjectImageScale / 3, groundLayer);
        
        

    }
}
