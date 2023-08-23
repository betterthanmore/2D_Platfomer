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
    public float timer;
    private SpriteRenderer sr;

    protected GameManager GameManager => GameManager.Instance;


    // Start is called before the first frame update
    void Start()
    {
        portalText = GameObject.Find("NextStage").GetComponent<Text>();
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
            if ((Input.GetKeyDown(KeyCode.S) || Input.GetButtonDown("GamePad1_Y")) && Physics2D.OverlapCircle(transform.position, portalImgScale / 3, 256))    //���� �÷��̾� ��ư//���߿� Ű ����
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

            if ((Input.GetKeyDown(KeyCode.DownArrow) || Input.GetButtonDown("GamePad2_Y")) && Physics2D.OverlapCircle(transform.position, portalImgScale / 3, 256))         //���� �÷��̾� ��ư//���߿� Ű ����
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

            /*if (GameManager.gearItem >= 5)
            {
                
                
            }
            else
            {
                if (textFadeRun)
                {
                    if (Input.GetButtonDown("GamePad1_Y") || Input.GetButtonDown("GamePad2_Y"))                 //���߿� Ű ����
                    {
                        portalText.text = "�� ���ڶ��ϴ�. ���� ����: " + GameManager.remainGears;
                        StartCoroutine(TextFade());
                    } 
                }
            }*/

        }
        if(mainReady && subReady)           //����� ������ �غ� �ƴٸ�
        {
            /*if (GameManager.gearItem <= 7)
            {
                GameManager.gauge += 0.2f;
            }
            else if (GameManager.gearItem <= 9)
            {
                GameManager.gauge += 0.5f;
            }
            else
            {
                GameManager.gauge += 0.7f;
            }
            GameManager.remainGears = 5;
            GameManager.gearItem = 0;*/
            GameManager.gauge_Init = GameManager.gauge;
            if (GameManager.sceneName.Contains("Chap1"))
            {
                if(GameManager.sceneName == "Chap1_Tutorial_Stage1")
                {
                    SceneManager.LoadScene("Chap1_Stage1");
                }
                else if(GameManager.sceneName.Contains("Chap1_TA_Stage4"))
                {
                    GameManager.chapter1Num++;
                    SceneManager.LoadScene("Chapter1_Mode");
                }
                else
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }

            }
            else if (GameManager.sceneName.Contains("Chap2"))
            {
                if (GameManager.sceneName == "Chap2_Tutorial_Stage2")
                {
                    SceneManager.LoadScene("Chap2_Stage6");
                }
                else if(!GameManager.sceneName.Contains("Chap2_TA_Stage9"))
                {
                    GameManager.chapter2Num++;
                    SceneManager.LoadScene("Chapter2_Mode");
                }
                else
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


            }
            else if (GameManager.sceneName.Contains("Chap3"))
            {
                if (GameManager.sceneName == "Chap3_Tutirial_Stage3")
                {
                    SceneManager.LoadScene("Chap3_Stage10");
                }
                else if(!GameManager.sceneName.Contains("Chap3_TA_Stage13"))
                {
                    GameManager.chapter3Num++;
                    SceneManager.LoadScene("Chapter3_Mode");
                }
                else
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


            }

            mainReady = false;
            subReady = false;
        }
    }
    IEnumerator TextFade()
    {
        textFadeRun = false;
        portalText.DOFade(1f, 1f);
        yield return new WaitForSeconds(1.5f);
        portalText.DOFade(0f, 1f);
        yield return new WaitForSeconds(1.5f);
        textFadeRun = true;
    }

}
