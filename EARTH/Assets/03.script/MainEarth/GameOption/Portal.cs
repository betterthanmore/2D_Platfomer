using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class Portal : MonoBehaviour
{

    public bool isPlayer;
    public float portalImgScale;
    public LayerMask player;
    public Text portalText;
    public bool textFadeRun = true;                           //�÷��̾�� ���� �÷��� �� �ش� ������ �˷��ֱ� ���� ��� �۾�
    public bool subReady = false;                       //��Ż�� �Ѿ�� ���� ����ĳ�� �Ҹ��� ��
    public bool mainReady = false;                      //��Ż�� �Ѿ�� ���� ����ĳ�� �Ҹ��� ��
    public Scrollbar scrollbar;
    private float flipTime;
    private float timer;
    private SpriteRenderer sr;

    protected GameManager GameManager => GameManager.Instance;


    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.15f)
        {
            sr.flipY = true;
            if(timer >= 0.3f)
            {
                timer = 0;
            }
        }
        else
        {
            sr.flipY = false;
        }

        isPlayer = Physics2D.OverlapCircle(transform.position, portalImgScale / 3, player);
        if (isPlayer)
        {
            Debug.Log("�÷��̾� ����");
            if (GameManager.gearItem >= 5)
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))    //���� �÷��̾� ��ư//���߿� Ű ����
                {
                    
                    if (!mainReady)
                    {
                        mainReady = true;                    //������ �̵��ϱ� ���� �غ��ư

                    }
                    else
                    {
                        if (!subReady)
                        {
                            portalText.text = "2P�� ���� ��ŻŰ�� ������ �ʾҽ��ϴ�.";
                            StartCoroutine(TextFade());
                        }
                    }
                }
                if (Input.GetKeyDown(KeyCode.S))            //���� �÷��̾� ��ư//���߿� Ű ����
                {
                    if (!subReady)
                    {
                        subReady = true;                    //������ �̵��ϱ� ���� �غ��ư
                    }
                    else
                    {
                        if (!mainReady)
                        {
                            portalText.text = "1P�� ���� ��ŻŰ�� ������ �ʾҽ��ϴ�.";
                            StartCoroutine(TextFade());
                        }
                    }
                }
            }
            else
            {
                Debug.Log("else�� ���");
                if (textFadeRun)
                {
                    Debug.Log("���̵� ���");
                    if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))                 //���߿� Ű ����
                    {
                        portalText.text = "�� ���ڶ��ϴ�. ���� ����: " + GameManager.mixGears;
                        StartCoroutine(TextFade());
                    } 
                }
            }

        }
        if(mainReady && subReady)           //����� ������ �غ� �ƴٸ�
        {
            if(GameManager.gearItem <= 7)
            {
                scrollbar.size += 0.2f;
            }
            else if(GameManager.gearItem <= 9)
            {
                scrollbar.size += 0.5f;
            }
            else
            {
                scrollbar.size += 0.7f;
            }
            GameManager.gearItem = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            mainReady = false;
            subReady = false;
        }
    }
    IEnumerator TextFade()
    {
        Debug.Log("���̵� ����");
        textFadeRun = false;
        portalText.DOFade(1f, 1f);
        yield return new WaitForSeconds(1.5f);
        portalText.DOFade(0f, 1f);
        yield return new WaitForSeconds(1.5f);
        textFadeRun = true;
    }

}
