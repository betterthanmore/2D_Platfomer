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
    public float timer;
    private SpriteRenderer sr;

    protected GameManager GameManager => GameManager.Instance;


    // Start is called before the first frame update
    void Start()
    {
        portalText = GameObject.Find("NextStage").GetComponent<Text>();
        scrollbar = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer <= 0.10f)
        {
            sr.flipX = false;
            
        }
        else if (timer <= 0.20f)
        {
            sr.flipY = true;
            
        }
        else if (timer <= 0.30f)
        {
            sr.flipX = true;
            
        }
        else if (timer >= 0.40f)
        {
            timer = 0;

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
                if (Input.GetButtonDown("GamePad1_Y"))    //���� �÷��̾� ��ư//���߿� Ű ����
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
                if (Input.GetButtonDown("GamePad2_Y"))            //���� �÷��̾� ��ư//���߿� Ű ����
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
                    if (Input.GetButtonDown("GamePad1_Y") || Input.GetButtonDown("GamePad2_Y"))                 //���߿� Ű ����
                    {
                        portalText.text = "�� ���ڶ��ϴ�. ���� ����: " + GameManager.remainGears;
                        StartCoroutine(TextFade());
                    } 
                }
            }

        }
        if(mainReady && subReady)           //����� ������ �غ� �ƴٸ�
        {
            if(GameManager.gearItem <= 7)
            {
                GameManager.gauge += 0.2f;
            }
            else if(GameManager.gearItem <= 9)
            {
                GameManager.gauge += 0.5f;
            }
            else
            {
                GameManager.gauge += 0.7f;
            }
            GameManager.remainGears = 5;
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
