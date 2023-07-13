using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainKeyBoard : PlayerMainController
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

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            if (rayHit)
            {
                Debug.Log("감지");
                otherVelocity = rayHit.GetComponent<Rigidbody2D>().velocity.x;
                rb.velocity = new Vector2(Input.GetAxis("HorizontalMain") * moveSpeed + otherVelocity, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(Input.GetAxis("HorizontalMain") * moveSpeed, rb.velocity.y);
            }
            if (rb.velocity.x < 0)                  //이미지 좌우 반전 옮김
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
        else
        {
            if (rayHit)
            {
                otherVelocity = rayHit.GetComponent<Rigidbody2D>().velocity.x;
                rb.velocity = new Vector2(otherVelocity, rb.velocity.y);
            }
        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && (isPlayerOn || isGround))
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);
        }
        if (gameObject.tag == "MainPlayer" && Input.GetKeyUp(KeyCode.LeftArrow) || gameObject.tag == "MainPlayer" && Input.GetKeyUp(KeyCode.RightArrow) //메인플레이어가 좌,우로 움직임을 멈출 때 바로 멈추게 하기
        || gameObject.tag == "SubPlayer" && Input.GetKeyUp(KeyCode.A) || gameObject.tag == "SubPlayer" && Input.GetKeyUp(KeyCode.D))                    //서브플레이어가 좌,우로 움직임을 멈출 때 바로 멈추게 하기
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            an.SetBool("Run", false);        //걷는 모션 중지

        }
        if (!isGround && isPlayerOn || isGround && !isPlayerOn)      //땅을 밟고 있지 않다면 걷는 모션 중지
        {
            an.SetBool("Jump", false);
        }
        else//땅을 밟고 있다면 모션 점프 중지
        {
            an.SetBool("Run", false);
            an.SetBool("Jump", true);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)     //톱니바퀴에 관한 코드
    {
        if (other.gameObject.tag == "GearItem")
        {
            Destroy(other.gameObject);
            GameManager.gearItem += 1;
            if(GameManager.gearItem < 5)
            {
                GameManager.remainGears -= 1;
                StartCoroutine(GameManager.MinimumGears());           
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BreakAbleObject")
        {
            collision.gameObject.GetComponent<BreakAbleObject>().DoFadeInOut();
        }

    }

}
