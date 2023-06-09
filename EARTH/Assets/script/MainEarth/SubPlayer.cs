using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubPlayer : PlayerMainController
{
    public float boostDistanceLimit = 1;
    public Scrollbar scr;
    public int boostTime = 0;           //부스트 시간
    public float boostDistance = 0;     //부스트로 한번에 올라갈 수 있는 거리
    private float subplayerPosY;    //부스트 사용할 때의 플레이어의 위치 저장
    private bool enableBoost = true;            //부스트 사용 가능 여부
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.W) && enableBoost)     //땅에 닿고 부스트 키를 눌렀을 때만 플레이어의 Y축 값을 저장한다.
        {
            subplayerPosY = gameObject.transform.position.y;
        }
        if (gameObject.tag == "SubPlayer" && Input.GetKey(KeyCode.W) && rb.velocity.y <= boostDistanceLimit && scr.value > 0.001 
            && enableBoost /*여기에 플레이어가 올라갈 수 있는 거리에 제한 두기*/)        //부스트
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            scr.value -= 1 * Time.fixedDeltaTime /boostTime;
        }
        if (isGround)   //땅에 닿는다면
        {
            enableBoost = true;
        }
        else           //땅에서 떨어진다면
        {
            enableBoost = false;
        }
        
    }
}
