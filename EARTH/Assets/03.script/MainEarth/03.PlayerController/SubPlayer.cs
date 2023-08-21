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
    public bool maxDistance = false;            //�ν�Ʈ�� �ö� �Ÿ��� �ִ�ġ�� ���� �� �ڵ����� ��������

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
                if (Input.GetButtonDown(JumpKeyMap) && enableBoost || Input.GetKeyDown(KeyCode.UpArrow) && enableBoost)     //���� ��� �ν�Ʈ Ű�� ������ ���� �÷��̾��� Y�� ���� �����Ѵ�.
                {
                    state = State.MOVE;
                    subplayerPosYCrt = gameObject.transform.position.y;
                }
                if (Input.GetButtonUp(JumpKeyMap) || maxDistance || Input.GetKeyUp(KeyCode.UpArrow))  //����Ű�� 
                {
                    state = State.IDEL;
                    enableBoost = false;
                }
            }

            if (gameObject.tag == "SubPlayer" && (Input.GetButton(JumpKeyMap) || Input.GetKey(KeyCode.UpArrow)) && scrollbar.size > 0.001 && !maxDistance && enableBoost)        //�ν�Ʈ
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
        if (isStepOn)   //���� ��´ٸ�
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
        Debug.Log("�ڷ�ƾ ����");
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
