using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubPlayer : PlayerMainController
{
    public static Scrollbar scrollbar;
    public int boostTime = 0;           //�ν�Ʈ �ð�
    public float boostDistanceLimit = 1;//�ν�Ʈ�� �ѹ��� �ö� �� �ִ� �Ÿ�
    public Camera cm;
    private float subplayerPosYCrt;    //�ν�Ʈ ����� ���� �÷��̾��� ���� ��ġ ����
    private float subPlayerPosYTrs;     //�ν�Ʈ ����ϸ鼭 �ٲ�� ��ġY ���� ����
    public bool enableBoost = true;            //�ν�Ʈ ��� ���� ����
    public static bool maxDistance = false;            //�ν�Ʈ�� �ö� �Ÿ��� �ִ�ġ�� ���� �� �ڵ����� ��������
    public float boxPosX;               //�ڽ� x������ �����ִ� ����
    /*public Vector2 boxPos;            //�ڽ� ������ ��ġ ���� �޴� ����
    public Vector2 boxPosInit;*/        //�ڽ� ����� �� �ʱ갪
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
                if ((Input.GetButtonDown(JumpKeyMap) && enableBoost) || (Input.GetKeyDown(KeyCode.UpArrow) && enableBoost))     //���� ��� �ν�Ʈ Ű�� ������ ���� �÷��̾��� Y�� ���� �����Ѵ�.
                {
                    Debug.Log("�ٿ� Ű ���� ����");
                    state = State.MOVE;
                    subplayerPosYCrt = gameObject.transform.position.y;
                }

                if (Input.GetButtonUp(JumpKeyMap) || maxDistance || Input.GetKeyUp(KeyCode.UpArrow))  //����Ű�� 
                {
                    enableBoost = false;
                }

                if (gameObject.tag == "SubPlayer" && (Input.GetButton(JumpKeyMap) || Input.GetKey(KeyCode.UpArrow)) && !maxDistance && enableBoost && scrollbar.size > 0.001)        //�ν�Ʈ
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

            /*if (boxSense && (Input.GetButtonDown("GamePad2_X") || Input.GetKeyDown(KeyCode.L))) �ڽ� ��� ���� 
            {
                subMove = false;
                HoldAction();
            }
            if (subBoxHold && subMove && (Input.GetButtonUp("GamePad2_X") || Input.GetKeyUp(KeyCode.L)))        //�ڽ� �������� ����
            {
                boxPosX = 0;
                PutAction();
            }*/

        }
        

        if (isStepOn)   //���� ��´ٸ�
        {
            enableBoost = true;
            maxDistance = false;
        }

        scrollbar.transform.position = cm.WorldToScreenPoint(new Vector2(transform.position.x, transform.position.y + 0.6f));

        AnimationTransform();


    }
    /*public void HoldAction()          �ڽ� �� �� �޼���
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

        
    /*public void PutAction()           //�ڽ� ���������� �� �޼���
    {
        state = State.IDEL;
        StartCoroutine("BoxPut");
    }
    IEnumerator OverHeadBox()           //�ڽ� �� �� �ڷ�ƾ
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
                    Debug.Log("�ڷ�ƾ ������ ����");
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
                    Debug.Log("�ڷ�ƾ ������ ����");
                    subMove = true;
                }
                yield return null;
            }
            putOnBox_L = true;
        }
    }*/
    /*IEnumerator BoxPut()      //�ڽ� ���� ���� �ڵ�
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
                Debug.Log("�ʱⰪ: " + boxPosInit);
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
                        Debug.Log("������ �������� ����");
                        subMove = true;
                        PlayerStateInit();
                    }
                }
                else
                {
                    if (boxPos == new Vector2(-boxPosInit.x, boxPosInit.y))
                    {
                        Debug.Log("������ �������� ����");
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
                        Debug.Log("���� �������� ����");
                        subMove = true;
                        PlayerStateInit();
                    }
                }
                else
                {
                    if (boxPos == new Vector2(boxPosInit.x, boxPosInit.y))
                    {
                        Debug.Log("���� �������� ����");
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
        Debug.Log("����");
        transform.GetChild(2).gameObject.layer = 11;
        transform.GetChild(2).GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        transform.GetChild(2).transform.parent = null;
        subBoxHold = false;
        subMove = true;
        moveSpeed = 2;
    }*/

}
