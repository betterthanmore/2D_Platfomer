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
    public bool subMove = true;
    public float boxPosX;
    public Vector2 boxPos;

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

        /*if (subMove)
        {
        }*/
        if (!subBoxHold)
        {
            if (rb.velocity.x == 0 && (isStepOn || isPlayerOn))
            {
                state = State.IDEL;
            }
            else
            {
                state = State.MOVE;
            }
        }

        subPlayerPosYTrs = gameObject.transform.position.y;
        if (GameManager.move)
        {
            
            if (!subBoxHold)
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

            if (boxSense && (Input.GetButtonDown("GamePad2_X") || Input.GetKeyDown(KeyCode.L)))
            {
                subMove = false;
                HoldAction();
            }
            if (subBoxHold && subMove && (Input.GetButtonUp("GamePad2_X") || Input.GetKeyUp(KeyCode.L)))
            {
                boxPosX = 0;
                PutAction();
            }

        }
        if (boxPos == new Vector2(0, 0.8f))
        {
            Debug.Log("코루틴 마지막 실행");
            subMove = true;
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
        subBoxHold = true;
        state = State.SUBHOLD;
        boxSense.collider.transform.parent = gameObject.transform;
        boxSense.collider.gameObject.layer = 3;
        boxSense.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        Vector2 boxPos = transform.GetChild(2).localPosition;
        StartCoroutine("OverHeadBox");
    }

        
    public void PutAction()
    {
        state = State.IDEL;
        StartCoroutine("Put");
    }
    IEnumerator OverHeadBox()
    {
        boxPos = transform.GetChild(2).localPosition;
        if (dir == 1)
        {
            while (boxPos.x > 0 || boxPos.y < 0.8f)
            {
                boxPosX += Time.deltaTime * 0.05f;
                boxPos.x = Mathf.Clamp(boxPos.x - boxPosX, 0, 0.6f);
                if(boxPos.y < 0.8f)
                {
                    boxPos.y = Mathf.Clamp(boxPos.y + 0.8f * Time.deltaTime, 0, 0.8f);
                }
                Debug.Log(boxPos.y);
                boxPos = boxPos.normalized * 0.8f;
                transform.GetChild(2).localPosition = boxPos;
                yield return null;
            }
        }
        else if (dir == -1)
        {
            while (boxPos.x < 0 || boxPos.y < 0.8f)
            {
                boxPosX += Time.deltaTime * 0.05f;
                boxPos.x = Mathf.Clamp(boxPos.x + boxPosX, -0.6f, 0);
                boxPos.y = Mathf.Clamp(boxPos.y + 0.8f * Time.deltaTime, 0, 0.8f);
                boxPos = boxPos.normalized * 0.8f;
                transform.GetChild(2).localPosition = boxPos;
                yield return null;
            }
        }

        
    }
    IEnumerator Put()
    {       //추가 수정해야됨
        boxPos = transform.GetChild(2).localPosition;

        if (dir == 1)
        {
            while (boxPos.x >= Mathf.Clamp(boxPos.x, 0, 0.6f) && boxPos.y <= Mathf.Clamp(boxPos.y, 0, 0.8f))
            {
                
                boxPosX -= Time.deltaTime;
                boxPos += new Vector2(-0.5f * dir, 0.6f).normalized * Mathf.Rad2Deg * Time.deltaTime * 2;
                transform.GetChild(2).localPosition = boxPos;
                yield return null;
            }
        }
        else if (dir == -1)
        {
            while (boxPos.x >= Mathf.Clamp(boxPos.x, -0.06f, 0) && boxPos.y <= Mathf.Clamp(boxPos.y, 0, 0.8f))
            {
                boxPos += new Vector2(-0.5f * dir, 0.4f).normalized * Time.deltaTime * 2;
                transform.GetChild(2).localPosition = boxPos;
                yield return null;
            }

        }

        if (boxPos.x > 0 || boxPos.y < 0.8f)
        {
            boxPos = new Vector2(0, 0.8f);
        }


        if (boxPos == new Vector2(0, 0.8f))
        {
            subMove = true;
        }

        while (boxPos.x >= Mathf.Clamp(boxPos.x, -0.6f, 0.6f) && boxPos.y >= Mathf.Clamp(boxPos.y, 0, 0.9f))
        {
            boxPos += new Vector2(0.5f * dir, -0.4f).normalized* Time.deltaTime * 2;
            transform.GetChild(2).localPosition = boxPos;
            yield return null;

        }
        yield return new WaitForSeconds(1.5f);
        transform.GetChild(2).gameObject.layer = 11;
        transform.GetChild(2).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        subBoxHold = false;
        transform.GetChild(2).transform.parent = null;
        moveSpeed = 2;
    }

}
