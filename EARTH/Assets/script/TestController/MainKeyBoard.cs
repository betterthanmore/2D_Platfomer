using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainKeyBoard : PlayerMainController
{
    public bool isPlayerOn;
    public static int sawtoothWheelNum = 0;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
        isPlayerOn = Physics2D.OverlapCircle(groundCheck.position, 0.2f, playerLayer);  //플레이어 머리를 밟고 있다면

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(Input.GetAxis("HorizontalMain") * moveSpeed, rb.velocity.y);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && (isPlayerOn || isGround))
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
        if(other.gameObject.tag == "SawtoothWheel")
        {
            Destroy(other);
            sawtoothWheelNum++;
        }
    }

}
