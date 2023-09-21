using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SubPlayer : PlayerMainController
{
    public static Scrollbar scrollbar;
    public int boostTime = 0;           //부스트 시간
    public float boostDistanceLimit = 1;//부스트로 한번에 올라갈 수 있는 거리
    public Camera cm;
    private Vector2 boostPower;
    private float subplayerPosYCrt;    //부스트 사용할 때의 플레이어의 현재 위치 저장
    private float subPlayerPosYTrs;     //부스트 사용하면서 바뀌는 위치Y 값을 저장
    public bool enableBoost = true;            //부스트 사용 가능 여부
    public bool boostKeyDown = false;
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
        if (objectSense)
        {
            Debug.Log(objectSense.collider.gameObject.layer);
        }
        subPlayerPosYTrs = gameObject.transform.position.y;

        if (enableBoost && scrollbar.size > 0.001 && boostKeyDown)        //부스트
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            scrollbar.size -= 1 * Time.deltaTime / boostTime;
            GameManager.gauge = scrollbar.size;
            if (subPlayerPosYTrs >= subplayerPosYCrt + boostDistanceLimit)
            {
                enableBoost = false;
            }
        }
        /*if (GameManager.move)
        {
            if (boxSense && (Input.GetButtonDown("GamePad2_X") || Input.GetKeyDown(KeyCode.L))) 박스 들기 실행
            {
                subMove = false;
                HoldAction();
            }
            if (subBoxHold && subMove && (Input.GetButtonUp("GamePad2_X") || Input.GetKeyUp(KeyCode.L)))        //박스 내려놓기 실행
            {
                boxPosX = 0;
                PutAction();
            }
        }*/


        if (isStepOn)   //땅에 닿는다면
        {
            enableBoost = true;
            subplayerPosYCrt = gameObject.transform.position.y;
        }

        scrollbar.transform.position = cm.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + 0.6f));

        AnimationTransform();


    }
    public void Boost(InputAction.CallbackContext input)
    {
        if (GameManager.move)
        {
            if (input.started && !subBoxHold && enableBoost && (input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard") && isStepOn)
            {
                boostKeyDown = true;
                state = State.MOVE;
            }
            else if (input.canceled && (input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard"))
            {
                boostKeyDown = false;
            } 
        }
    }
    public void IronBreak(InputAction.CallbackContext input)        //이거 게임매니저로 옮겨야됨. 이유는 코루틴이 끝나고 if문이 도는 것 같음 집에서 테스트 해봐야됨
    {
        if (objectSense.collider == null)
            return;
        else if (objectSense.collider.gameObject.layer == 10 && input.started && GameManager.reGameButtonDown && GameManager.move &&
            (input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard"))
        {       //콜라이더를 검출해내는거라 자식 빈오브젝트에 넣어둔 철근들 다 수정해야됨ㅋㅋ
            GameManager.move = false;
            StartCoroutine(UIManager.FadeScreenSetUp());

        }
    }
    
    public void ReLoad(InputAction.CallbackContext input)
    {
        if (input.started && GameManager.reGameButtonDown && 
            (input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard"))
        {
            GameManager.reGame2P = true;
            if (GameManager.reGame1P && GameManager.reGame2P)
            {
                GameManager.reGameButtonDown = false;
                GameManager.ReGameStart();
            }
        }
    }
    public void Portar(InputAction.CallbackContext input)
    {
        if ((GameManager.portalOnPlayer && input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard") && input.started)
        {
            if (GameManager.portal_Ready_Player[1])
            {
                StartCoroutine(UIManager.MinimumGears("소녀가 준비 될 때까지 기다려주자"));
            }
            else
                GameManager.PortalAction();
        }
    }
    public void Lever(InputAction.CallbackContext input)
    {
        if (input.control.device.name == ControllerDevices || input.control.device.name == "Keyboard" && input.started)
            GameManager.SubLever();
    }
    
    /*public void BoostKeyHold(InputAction.CallbackContext input)
    {
        if (input.control.parent.name == ControllerDevices)
        {

            if (input.started)
            {
                if (enableBoost)
                {
                    state = State.MOVE;
                    subplayerPosYCrt = gameObject.transform.position.y;
                }
            }
            else if (input.performed)
            {
                Debug.Log(input.GetType());
                Debug.Log(input.valueType);
                Debug.Log(input.ReadValueAsButton());
                Debug.Log(input.ReadValue<System.Single>());
                if (!subBoxHold)
                {
                    if (gameObject.tag == "SubPlayer" && enableBoost && scrollbar.size > 0.001)        //부스트
                    {
                        boostPower.y = input.ReadValue<System.Single>() * jumpForce;
                        scrollbar.size -= 1 * Time.deltaTime / boostTime;
                        GameManager.gauge = scrollbar.size;
                    }
                }
            }
            else if (input.canceled)
            {
                enableBoost = false;
                Debug.Log("뗌");

            }

        }


    }*/
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
