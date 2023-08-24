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
    public static bool maxDistance = false;            //부스트로 올라간 거리가 최대치가 됐을 때 자동으로 떨어지게
    public float boxPosX;               //박스 x포지션 정해주는 변수
    /*public Vector2 boxPos;            //박스 포지션 위치 값을 받는 변수
    public Vector2 boxPosInit;*/        //박스 잡았을 때 초깃값
    public bool putOnBox_L = false;

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

        if (subMove)
        {
            base.Update();
        }
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
                if ((Input.GetButtonDown(JumpKeyMap) && enableBoost) || (Input.GetKeyDown(KeyCode.UpArrow) && enableBoost))     //땅에 닿고 부스트 키를 눌렀을 때만 플레이어의 Y축 값을 저장한다.
                {
                    Debug.Log("다운 키 잠프 반응");
                    state = State.MOVE;
                    subplayerPosYCrt = gameObject.transform.position.y;
                }

                if (Input.GetButtonUp(JumpKeyMap) || maxDistance || Input.GetKeyUp(KeyCode.UpArrow))  //점프키를 
                {
                    enableBoost = false;
                }

                if (gameObject.tag == "SubPlayer" && (Input.GetButton(JumpKeyMap) || Input.GetKey(KeyCode.UpArrow)) && !maxDistance && enableBoost && scrollbar.size > 0.001)        //부스트
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

            /*if (boxSense && (Input.GetButtonDown("GamePad2_X") || Input.GetKeyDown(KeyCode.L))) 박스 들기 실행 
            {
                subMove = false;
                HoldAction();
            }
            if (subBoxHold && subMove && (Input.GetButtonUp("GamePad2_X") || Input.GetKeyUp(KeyCode.L)))        //박스 내려놓기 실행
            {
                boxPosX = 0;
                PutAction();
            }*/

        }
        

        if (isStepOn)   //땅에 닿는다면
        {
            enableBoost = true;
            maxDistance = false;
        }

        scrollbar.transform.position = cm.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + 0.6f));

        AnimationTransform();


    }
    /*public void HoldAction()          박스 들 때 메서드
    {
        subBoxHold = true;
        state = State.SUBHOLD;
        boxSense.collider.transform.parent = gameObject.transform;
        boxPosInit = transform.GetChild(2).localPosition;
        boxSense.collider.gameObject.layer = 3;
        boxSense.collider.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        Vector2 boxPos = transform.GetChild(2).localPosition;
        StartCoroutine("OverHeadBox");
    }*/

        
    /*public void PutAction()           //박스 내려놓았을 때 메서드
    {
        state = State.IDEL;
        StartCoroutine("BoxPut");
    }
    IEnumerator OverHeadBox()           //박스 들 때 코루틴
    {
        boxPos = transform.GetChild(2).localPosition;
        if (dir == 1)
        {
            while (boxPos.x != 0 && boxPos.y != 0.8f)
            {
                boxPosX += Time.deltaTime * 0.05f;
                boxPos.x = Mathf.Clamp(boxPos.x - boxPosX, 0, 0.6f);
                boxPos.y = Mathf.Clamp(boxPos.y + 0.8f * Time.deltaTime, 0, 0.8f);
                boxPos = boxPos.normalized * 0.8f;
                transform.GetChild(2).localPosition = boxPos;
                if (boxPos == new Vector2(0, 0.8f))
                {
                    Debug.Log("코루틴 마지막 실행");
                    subMove = true;
                }
                yield return null;
            }
            putOnBox_L = false;
        }
        else if (dir == -1)
        {
            while (boxPos.x != 0 && boxPos.y != 0.8f)
            {
                boxPosX += Time.deltaTime * 0.05f;
                boxPos.x = Mathf.Clamp(boxPos.x + boxPosX, -0.6f, 0);
                boxPos.y = Mathf.Clamp(boxPos.y + 0.8f * Time.deltaTime, 0, 0.8f);
                boxPos = boxPos.normalized * 0.8f;
                transform.GetChild(2).localPosition = boxPos;
                if (boxPos == new Vector2(0, 0.8f))
                {
                    Debug.Log("코루틴 마지막 실행");
                    subMove = true;
                }
                yield return null;
            }
            putOnBox_L = true;
        }
    }*/
    /*IEnumerator BoxPut()      //박스 내려 놓기 코드
    {       
        boxPos = transform.GetChild(2).localPosition;

        if (dir == 1)
        {
            if (!putOnBox_L)
                boxPos = boxPosInit;

            else
                boxPos = -boxPosInit;

            yield return null;
            PlayerStateInit();
            *//*while (boxPos != boxPosInit)
            {
                Debug.Log("초기값: " + boxPosInit);
                Debug.Log(boxPos);
                boxPosX += Time.deltaTime * 0.01f;
                if (!putOnBox_L)
                {
                    *//*boxPos = new Vector2(Mathf.Lerp(0, boxPosInit.x, 1), Mathf.Clamp(boxPos.y - 0.8f * Time.deltaTime, boxPosInit.y, 0.8f));*//*
                    if (boxPos.x != boxPosInit.x)
                        boxPos.x = Mathf.Clamp(boxPos.x + boxPosX, 0, boxPosInit.x);
                }
                
                else
                {
                    *//*boxPos = new Vector2(Mathf.Lerp(0, -boxPosInit.x, 1), Mathf.Clamp(boxPos.y - 0.8f * Time.deltaTime, boxPosInit.y, 0.8f));*//*
                    if (boxPos.x != -boxPosInit.x)
                        boxPos.x = Mathf.Clamp(boxPos.x + boxPosX, 0, -boxPosInit.x);

                }
                if (boxPos.y != boxPosInit.y)
                    boxPos.y = Mathf.Clamp(boxPos.y - 5f * Time.deltaTime, boxPosInit.y, 0.8f);

                boxPos = boxPos.normalized * 0.8f;
                transform.GetChild(2).localPosition = boxPos;
                if (!putOnBox_L)
                {
                    if (boxPos == new Vector2(boxPosInit.x, boxPosInit.y))
                    {
                        Debug.Log("오른쪽 내려놓기 진입");
                        subMove = true;
                        PlayerStateInit();
                    }
                }
                else
                {
                    if (boxPos == new Vector2(-boxPosInit.x, boxPosInit.y))
                    {
                        Debug.Log("오른쪽 내려놓기 진입");
                        subMove = true;
                        PlayerStateInit();
                    }
                }
                yield return null;
            }*//*
        }
        else if (dir == -1)
        {
            if(!putOnBox_L)
                boxPos = -boxPosInit;

            else
                boxPos = boxPosInit;

            yield return null;
            PlayerStateInit();


            *//*while (boxPos!= boxPosInit)
            {
                Debug.Log(boxPos);
                *//*boxPos = boxPos.normalized * 0.8f;*//*
                boxPosX += Time.deltaTime * 0.05f;
                if (!putOnBox_L)
                {
                    *//*boxPos = new Vector2(Mathf.Lerp(0, -boxPosInit.x, 1), Mathf.Clamp(boxPos.y - 0.8f * Time.deltaTime, boxPosInit.y, 0.8f)).normalized;*//*
                    if (boxPos.x != -boxPosInit.x)
                    {
                        boxPos.x = Mathf.Clamp(boxPos.x - boxPosX, -boxPosInit.x, 0); 
                    }
                }
                else
                {
                    *//*boxPos = new Vector2(Mathf.Lerp(0, boxPosInit.x, 1), Mathf.Clamp(boxPos.y - 0.8f * Time.deltaTime, boxPosInit.y, 0.8f)).normalized;*//*
                    if (boxPos.x != boxPosInit.x)
                        boxPos.x = Mathf.Clamp(boxPos.x - boxPosX, boxPosInit.x, 0);
                }
                if(boxPos.y != boxPosInit.y)
                    boxPos.y = Mathf.Clamp(boxPos.y - 0.8f * Time.deltaTime, boxPosInit.y, 0.8f);

                boxPos = boxPos.normalized * 0.8f;
                transform.GetChild(2).localPosition = boxPos;

                if (!putOnBox_L)
                {
                    if (boxPos == new Vector2(-boxPosInit.x, boxPosInit.y))
                    {
                        Debug.Log("왼쪽 내려놓기 진입");
                        subMove = true;
                        PlayerStateInit();
                    }
                }
                else
                {
                    if (boxPos == new Vector2(boxPosInit.x, boxPosInit.y))
                    {
                        Debug.Log("왼쪽 내려놓기 진입");
                        subMove = true;
                        PlayerStateInit();
                    }
                }

                yield return null;
            }*//*
        }

    }
    public void PlayerStateInit()
    {
        Debug.Log("진입");
        transform.GetChild(2).gameObject.layer = 11;
        transform.GetChild(2).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        transform.GetChild(2).transform.parent = null;
        subBoxHold = false;
        subMove = true;
        moveSpeed = 2;
    }*/

}
