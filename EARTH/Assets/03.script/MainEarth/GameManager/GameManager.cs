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
    /*public GameObject player1;
    public GameObject player2;
    public Transform playerPos1;
    public Transform PlayerPos2;*/
    public GameObject minimumGears;
    private bool minimumbool = true;
    public Image fadeOutscreenBoard;           //페이드 아웃되는 이미지

    public static bool nextScene;       //여러번 눌렀을 때 실행되는 것을 막기 위해

    public int mixGears = 5;        //다음 맵으로 가기위한 조건용 변수(최소 5개를 먹어야되기 때문)
    public int gearItem;

    public static int num;

    private void Awake()
    {
        SceneManager.sceneLoaded += NextScene;
        num++;
        Debug.Log(num);
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
        Debug.Log("스타트 실행");
        
        


    }
    private void Update()
    {
        if (minimumGears != null)
        {
            minimumGears.GetComponent<Text>().text = mixGears + "개만 더 모으면 포탈 이동이 가능합니다.";
        }

    }
    public IEnumerator MinimumGears()
    {
        if (minimumbool)
        {
            minimumbool = false;
            minimumGears.GetComponent<Text>().DOFade(1f, 1f);
            yield return new WaitForSeconds(1.5f);
            minimumGears.GetComponent<Text>().DOFade(0f, 1f);
            yield return new WaitForSeconds(1.5f);
            minimumbool = true; 
        }

    }
    public void SelectScene()       //스테이지 버튼을 누르면
    {
        if (nextScene)          //여러번 눌렀을 때 실행되는 것을 막기 위해
        {
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
    public void NextScene(Scene arg, LoadSceneMode arg2)
    {
        if (!GameObject.Find("FadeOutscreenBoard"))         //페이드 아웃되는 이미지가 없다면
        {

        }
        nextScene = true;
        fadeOutscreenBoard = GameObject.Find("FadeOutscreenBoard").GetComponent<Image>();
        StartCoroutine(FadeScreen());
        Debug.Log("반응");
        minimumGears = GameObject.Find("MinimumGears");
    }

}
