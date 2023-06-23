using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //protected SceneChanger SceneChanger => SceneChanger.Instance;
    public GameManager() { }
    public static GameManager Instance { get; private set; }    //싱글톤 
    public GameObject minimumGears;
    public Image fadeOutscreenBoard;           //페이드 아웃되는 이미지
    public static bool nextScene;       //여러번 눌렀을 때 실행되는 것을 막기 위해
    public int mixGears = 5;        //다음 맵으로 가기위한 조건용 변수(최소 5개를 먹어야되기 때문)
    public int gearItem;
    private Canvas canvas;
    private bool nextSceneLoad1P = false;
    private bool nextSceneLoad2P = false;
    public bool playerMove = true;
    public float gauge = 1;
    public bool selectStage1 = true;
    public bool selectStage2 = false;
    public bool selectStage3 = false;
    

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
            nextScene = true;
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
                playerMove = false;
            } 
        }
        else
        {
            if (Input.GetButtonDown("GamePad1_B") || Input.GetButtonDown("GamePad2_B"))
            {
                Destroy(GameObject.Find("PAUSEBOARD(Clone)"));
                playerMove = true;
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
    public void SelectScene()       //스테이지 버튼을 누르면
    {
        if (nextScene)          //여러번 눌렀을 때 실행되는 것을 막기 위해
        {
            Debug.Log("반응");
            nextScene = false;
            StartCoroutine(SelectedSceneLoad());
        }
    }
    public IEnumerator SelectedSceneLoad()
    {
        fadeOutscreenBoard.gameObject.SetActive(true);
        fadeOutscreenBoard.DOFade(1, 1);
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public IEnumerator FadeScreen()
    {
        fadeOutscreenBoard.DOFade(0, 1);
        yield return new WaitForSeconds(1.1f);
        fadeOutscreenBoard.gameObject.SetActive(false);

    }
    
}
