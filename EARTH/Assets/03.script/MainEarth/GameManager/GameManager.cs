using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //protected SceneChanger SceneChanger => SceneChanger.Instance;
    public static GameManager Instance { get; private set; }    //싱글톤 
    public GameObject minimumGears;
    public Image fadeOutscreenBoard;           //페이드 아웃되는 이미지
    public int mixGears = 5;        //다음 맵으로 가기위한 조건용 변수(최소 5개를 먹어야되기 때문)
    public int gearItem;
    private Canvas canvas;
    private bool nextSceneLoad1P = false;
    private bool nextSceneLoad2P = false;
    public bool butttonBPress = false;
    public float gauge = 1;
    public bool selectStage1 = true;
    public bool selectStage2 = false;
    public bool selectStage3 = false;
    Sequence sequence;
    public GameObject soundWindow;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            transform.parent = null;
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
     
    }
    private void Start()
    {
            StartCoroutine(FadeScreen());
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        
        if (minimumGears != null)
        {
            if (mixGears > 0)
            {
                minimumGears.GetComponent<Text>().text = mixGears + "개만 더 모으면 포탈 이동이 가능합니다."; 
            }
            if(mixGears <= 0)
            {
                minimumGears.GetComponent<Text>().text = "포탈 이동이 가능합니다.";
            }
        }
        if(fadeOutscreenBoard == null)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            GameObject temp = Instantiate<GameObject>(Resources.Load<GameObject>("fadeOutscreenBoard"),canvas.transform);
            fadeOutscreenBoard = temp.GetComponent<Image>();
            StartCoroutine(FadeScreen());
            minimumGears = GameObject.Find("MinimumGears");
        }
        if (Input.GetButtonDown("GamePad2_X"))
        {
            nextSceneLoad2P = true;
        }
        if (Input.GetButtonDown("GamePad1_X"))
        {
            nextSceneLoad1P = true;
        }
        if(nextSceneLoad1P && nextSceneLoad2P)
        {
            nextSceneLoad1P = false;
            nextSceneLoad2P = false;
            mixGears = 5;
            gearItem = 0;
            gauge = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Time.timeScale != 0)
        {
            if (Input.GetButtonDown("GamePad1_B") || Input.GetButtonDown("GamePad2_B"))
            {
                Time.timeScale = 0;
                canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                GameObject temp2 = Instantiate<GameObject>(Resources.Load<GameObject>("PAUSEBOARD"), canvas.transform);
                butttonBPress = true;
            } 
        }
        else
        {
            if (Input.GetButtonDown("GamePad1_B") || Input.GetButtonDown("GamePad2_B"))
            {
                Destroy(GameObject.Find("PAUSEBOARD(Clone)"));
                butttonBPress = false;
                Time.timeScale = 1;
            }
        }
       
    }
    public IEnumerator MinimumGears()
    {
        minimumGears.GetComponent<Text>().DOFade(1f, 1f);
        yield return new WaitForSeconds(1.5f);
        minimumGears.GetComponent<Text>().DOFade(0f, 1f);
        yield return new WaitForSeconds(1.01f);
    }
    public IEnumerator FadeScreen()
    {
        fadeOutscreenBoard.DOFade(0, 1);
        yield return new WaitForSeconds(1.1f);
        fadeOutscreenBoard.gameObject.SetActive(false);
    }
    public void GamePab_B_Press()
    {

        if (!soundWindow.activeSelf)
        {
            soundWindow.SetActive(true);
            sequence = DOTween.Sequence();
            sequence.Append(soundWindow.transform.DOScale(new Vector2(0.6f, 0.6f), 0.1f));
            sequence.Append(soundWindow.transform.DOScale(new Vector2(0.5f, 0.5f), 0.1f));
        }
        else
        {
            soundWindow.transform.DOScale(new Vector2(0.1f, 0.1f), 0.1f);
            StartCoroutine(SoundWindowF());
        }

    }
    IEnumerator SoundWindowF()
    {
        yield return new WaitForSeconds(0.1f);
        soundWindow.SetActive(false);
    }

}
