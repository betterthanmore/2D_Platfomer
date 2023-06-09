using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubPlayer : PlayerMainController
{
    public Scrollbar scrollbar;
    public int boostTime = 0;           //부스트 시간
    public float boostDistanceLimit = 1;//부스트로 한번에 올라갈 수 있는 거리
    public Camera cm;
    private float subplayerPosYCrt;    //부스트 사용할 때의 플레이어의 현재 위치 저장
    private float subPlayerPosYTrs;     //부스트 사용하면서 바뀌는 위치Y 값을 저장
    private bool enableBoost = true;            //부스트 사용 가능 여부
    private bool maxDistance = true;            //부스트로 올라간 거리가 최대치가 됐을 때 자동으로 떨어지게

    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        subPlayerPosYTrs = gameObject.transform.position.y;
        if (Input.GetKeyDown(KeyCode.W) && enableBoost)     //땅에 닿고 부스트 키를 눌렀을 때만 플레이어의 Y축 값을 저장한다.
        {
            subplayerPosYCrt = gameObject.transform.position.y;
        }
        
        if (gameObject.tag == "SubPlayer" && Input.GetKey(KeyCode.W) && scrollbar.size > 0.001 && maxDistance)        //부스트
        {
            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            scrollbar.size -= 1 * Time.deltaTime /boostTime;
            if (subPlayerPosYTrs >= subplayerPosYCrt + boostDistanceLimit)
            {
                Debug.Log(subplayerPosYCrt);
                maxDistance = false;
            }
        }
        if (isGround)   //땅에 닿는다면
        {
            enableBoost = true;
            maxDistance = true;
        }
        else           //땅에서 떨어진다면
        {
            enableBoost = false;
        }
        scrollbar.transform.position = cm.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + 1.5f));

    }
}
