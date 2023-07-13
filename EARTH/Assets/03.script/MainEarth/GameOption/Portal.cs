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
    public bool textFadeRun = true;                           //플레이어에게 게임 플레이 중 해당 사항을 알려주기 위한 모든 글씨
    public bool subReady = false;                       //포탈을 넘어가기 위한 서브캐의 불리언 값
    public bool mainReady = false;                      //포탈을 넘어가기 위한 메인캐의 불리언 값
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
            Debug.Log("플레이어 닿음");
            if (GameManager.gearItem >= 5)
            {
                if (Input.GetButtonDown("GamePad1_Y"))    //메인 플레이어 버튼//나중에 키 변경
                {
                    
                    if (!mainReady)
                    {
                        mainReady = true;                    //다음씬 이동하기 위한 준비버튼

                    }
                    else
                    {
                        if (!subReady)
                        {
                            portalText.text = "2P가 아직 포탈키를 누르지 않았습니다.";
                            StartCoroutine(TextFade());
                        }
                    }
                }
                if (Input.GetButtonDown("GamePad2_Y"))            //서브 플레이어 버튼//나중에 키 변경
                {
                    if (!subReady)
                    {
                        subReady = true;                    //다음씬 이동하기 위한 준비버튼
                    }
                    else
                    {
                        if (!mainReady)
                        {
                            portalText.text = "1P가 아직 포탈키를 누르지 않았습니다.";
                            StartCoroutine(TextFade());
                        }
                    }
                }
            }
            else
            {
                Debug.Log("else문 통과");
                if (textFadeRun)
                {
                    Debug.Log("페이드 허용");
                    if (Input.GetButtonDown("GamePad1_Y") || Input.GetButtonDown("GamePad2_Y"))                 //나중에 키 변경
                    {
                        portalText.text = "기어가 모자랍니다. 남은 갯수: " + GameManager.remainGears;
                        StartCoroutine(TextFade());
                    } 
                }
            }

        }
        if(mainReady && subReady)           //서브랑 메인이 준비가 됐다면
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
        Debug.Log("페이드 실행");
        textFadeRun = false;
        portalText.DOFade(1f, 1f);
        yield return new WaitForSeconds(1.5f);
        portalText.DOFade(0f, 1f);
        yield return new WaitForSeconds(1.5f);
        textFadeRun = true;
    }

}
