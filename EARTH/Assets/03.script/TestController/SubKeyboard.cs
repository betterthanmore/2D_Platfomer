using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SubKeyboard : PlayerMainController
{
    public Scrollbar scrollbar;
    public int boostTime = 0;           //부스트 시간
    public float boostDistanceLimit = 1;//부스트로 한번에 올라갈 수 있는 거리
    public Camera cm;
    private float subplayerPosYCrt;    //부스트 사용할 때의 플레이어의 현재 위치 저장
    private float subPlayerPosYTrs;     //부스트 사용하면서 바뀌는 위치Y 값을 저장
    public bool enableBoost = true;            //부스트 사용 가능 여부
    private bool maxDistance = true;            //부스트로 올라간 거리가 최대치가 됐을 때 자동으로 떨어지게

    // Start is called before the first frame update
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
        if (Input.GetKeyUp(KeyCode.W) || !maxDistance)  //점프키를 뗐을 때 부스트를 못쓰게 한다 -> 플레이어가 땅에 찍을 때까지 
        {
            an.SetBool("Jump", false);
            enableBoost = false;
        }
        if (gameObject.tag == "SubPlayer" && Input.GetKey(KeyCode.W) && scrollbar.size > 0.001 && maxDistance && enableBoost)        //부스트 작동 시키는 코드
        {
            an.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            scrollbar.size -= 1 * Time.deltaTime / boostTime;
            if (subPlayerPosYTrs >= subplayerPosYCrt + boostDistanceLimit)
            {
                Debug.Log(subplayerPosYCrt);
                maxDistance = false;
            }
        }
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            rb.velocity = new Vector2(Input.GetAxis("HorizontalSub") * moveSpeed, rb.velocity.y);
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
        if (isGround)   //땅에 닿는다면
        {
            enableBoost = true;
            maxDistance = true;
        }
        else if(!isGround && !isPlayerOn)          //땅이랑 플레이어 모두 접촉중이지 않을 때
        {
            an.SetBool("Run", false);
        }

        if (gameObject.tag == "SubPlayer" && Input.GetKeyUp(KeyCode.A) || gameObject.tag == "SubPlayer" && Input.GetKeyUp(KeyCode.D))                    //서브플레이어가 좌,우로 움직임을 멈출 때 바로 멈추게 하기 이거 조이스틱은 없어도 됨
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            an.SetBool("Run", false);        //걷는 모션 중지

        }
        scrollbar.transform.position = cm.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + 1.5f));
    }
}
