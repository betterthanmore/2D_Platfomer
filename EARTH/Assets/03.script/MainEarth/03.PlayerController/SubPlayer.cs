using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubPlayer : PlayerMainController
{
    public static Scrollbar scrollbar;
    public int boostTime = 0;           //부스트 시간
    public float boostDistanceLimit = 1;//부스트로 한번에 올라갈 수 있는 거리
    public Camera cm;
    private float subplayerPosYCrt;    //부스트 사용할 때의 플레이어의 현재 위치 저장
    private float subPlayerPosYTrs;     //부스트 사용하면서 바뀌는 위치Y 값을 저장
    public bool enableBoost = true;            //부스트 사용 가능 여부
    public bool maxDistance = false;            //부스트로 올라간 거리가 최대치가 됐을 때 자동으로 떨어지게

    public override void Start()
    {

        base.Start();
        cm = GameObject.Find("Main Camera").GetComponent<Camera>();
        scrollbar = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
        scrollbar.size = GameManager.gauge;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        subPlayerPosYTrs = gameObject.transform.position.y;
        if (GameManager.move)
        {
            if (boxSense)
            {
                HoldAction();
            }
            if (!boxHold)
            {
                if (Input.GetButtonDown(JumpKeyMap) && enableBoost || Input.GetKeyDown(KeyCode.UpArrow) && enableBoost)     //땅에 닿고 부스트 키를 눌렀을 때만 플레이어의 Y축 값을 저장한다.
                {
                    state = State.MOVE;
                    subplayerPosYCrt = gameObject.transform.position.y;
                }
                if (Input.GetButtonUp(JumpKeyMap) || maxDistance || Input.GetKeyUp(KeyCode.UpArrow))  //점프키를 
                {
                    state = State.IDEL;
                    enableBoost = false;
                }
            }

            if (gameObject.tag == "SubPlayer" && (Input.GetButton(JumpKeyMap) || Input.GetKey(KeyCode.UpArrow)) && scrollbar.size > 0.001 && !maxDistance && enableBoost)        //부스트
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                scrollbar.size -= 1 * Time.deltaTime / boostTime;
                GameManager.gauge = scrollbar.size;
                if (subPlayerPosYTrs >= subplayerPosYCrt + boostDistanceLimit)
                {
                    maxDistance = true;
                }
            }
            
        }
        if (isStepOn)   //땅에 닿는다면
        {
            enableBoost = true;
            maxDistance = false;
        }

        scrollbar.transform.position = cm.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + 0.6f));

        AnimationTransform();


    }
    public void HoldAction()
    {
        if (Input.GetButtonDown("GamePad2_X") || Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine("OverHeadBox");
            
        }

        if (Input.GetButtonUp("GamePad2_X") || Input.GetKeyUp(KeyCode.L))
        {
            state = State.IDEL;
            boxHold = false;
            boxSense.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            boxSense.transform.parent = null;
            moveSpeed = 2;
        }
    }
    IEnumerator OverHeadBox()
    {
        Debug.Log("코루틴 반응");
        state = State.HOLD;
        boxHold = true;
        boxSense.collider.transform.parent = gameObject.transform;
        boxSense.collider.gameObject.layer = 3;
        boxSense.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        Vector2 boxPos = boxSense.collider.transform.position;
        while (boxPos == new Vector2(Mathf.Clamp(boxPos.x, boxPos.x, 0), Mathf.Clamp(boxPos.y, boxPos.y, 0.6f)))
        {
            boxPos = new Vector2(Mathf.Clamp(boxPos.x, boxPos.x, 0) * Time.deltaTime, Mathf.Clamp(boxPos.y, boxPos.y, 0.6f) * Time.deltaTime);

        }
        yield return new WaitForSeconds(1);
    }
    /*IEnumerator Put()
    {

    }*/

}
