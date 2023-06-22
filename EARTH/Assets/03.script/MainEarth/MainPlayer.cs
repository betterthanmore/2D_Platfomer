using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : PlayerMainController
{
    // Start is called before the first frame update
    public override void Start()
    {
        Debug.Log("작동");
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        
        if (gameObject.tag == "MainPlayer" && Input.GetButtonDown(JumpKeyMap) && (isGround || isPlayerOn))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
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
            if (GameManager.mixGears >= 0)
            {
                GameManager.mixGears -= 1;
                StopCoroutine(GameManager.MinimumGears());
                StartCoroutine(GameManager.MinimumGears());           //6월 20일 추가하고 아직 조이스틱용 스크립트엔 안넣음 넣으면 지울 것
            }
        }
    }
}
