using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //protected SceneChanger SceneChanger => SceneChanger.Instance;
    public static GameManager Instance { get; private set; }   
    public Text minimumGears;
    public Text timeAttack;
    private Text gameOverTA_text;
    private Text reGame_text;
    private Outline reGame_Outline;
    private Outline gameOverTA_Outline;
    public Image fadeOutscreenBoard;          
    public int remainGears = 5;       
    public int gearItem = 0;
    private Canvas canvas;
    private bool nextSceneLoad1P = false;
    private bool nextSceneLoad2P = false;
    public bool butttonBPress = false;
    public float gauge = 1;
    public bool selectStage1 = true;
    public bool selectStage2 = false;
    public bool selectStage3 = false;
    public bool stage_TA = false;
    public bool donPress_B = false;
    public bool move = true;
    public bool notVidio = false;
    public float timeTAtime = 41;

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
            SceneManager.sceneLoaded += OnLoadSceneInfo;
        }

    }
    private void Start()
    {
        StartCoroutine(FadeScreen());
    }
    private void Update()
    {
        if (fadeOutscreenBoard == null)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            GameObject temp = Instantiate<GameObject>(Resources.Load<GameObject>("fadeOutscreenBoard"), canvas.transform);
            fadeOutscreenBoard = temp.GetComponent<Image>();
            StartCoroutine(FadeScreen());
            if (GameObject.Find("MinimumGears"))
            {
                minimumGears = GameObject.Find("MinimumGears").GetComponent<Text>();
            }
        }
        if (stage_TA)
        {
            TimeATTACK();
        }
        if (minimumGears != null)
        {
            if (remainGears > 0)
            {
                minimumGears.text = remainGears + "개만 더 먹으면 포탈 이동이 가능합니다.";
            }
            if (remainGears <= 0)
            {
                minimumGears.text = "포탈 이동이 가능합니다.";
            }
        }
        if (!butttonBPress)
        {
            if (Input.GetButtonDown("GamePad2_RT"))
            {
                nextSceneLoad2P = true;
            }
            if (Input.GetButtonDown("GamePad1_RT"))
            {
                nextSceneLoad1P = true;
            }
            if (Input.GetKeyDown(KeyCode.P) || nextSceneLoad1P && nextSceneLoad2P)
            {
                ForceNextStage();
            }
        }
        
        if (!donPress_B)
        {
            if (Input.GetButtonDown("GamePad1_B") || Input.GetButtonDown("GamePad2_B"))
            {
                if (!butttonBPress)
                {
                    butttonBPress = true;
                    move = false;
                    Time.timeScale = 0;
                }
                else
                {
                    butttonBPress = false;
                    move = true;
                    Time.timeScale = 1;
                }
            } 
        }
    }
    
    public void TimeATTACK()
    {
        
        if (timeTAtime > 0)
        {
            timeAttack.text = ((int)(timeTAtime -= Time.deltaTime)).ToString(); 
        }
        else
        {
            stage_TA = false;
            donPress_B = true;
            move = false;
            timeTAtime = 0;
            gameOverTA_text.DOFade(1, 1);
            gameOverTA_Outline.DOFade(1, 1);
            gameOverTA_text.gameObject.transform.DOLocalMove(Vector2.zero, 1);
            StartCoroutine(REGAME());
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
        donPress_B = true;
        yield return new WaitForSeconds(0.5f);
        fadeOutscreenBoard.DOFade(0, 1);
        yield return new WaitForSeconds(1.1f);
        fadeOutscreenBoard.gameObject.SetActive(false);
        if (notVidio)
        {
            donPress_B = false;
        }
    }
    public IEnumerator REGAME()
    {
        yield return new WaitForSeconds(1);
        reGame_text.DOFade(1, 1);
        reGame_Outline.DOFade(1, 1);

        for (int i = 3; i > -1; i--)
        {
            reGame_text.text = i + "초 후에 다시 시작합니다.";
            if(i == 0)
            {
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
        
    }
    public void ForceNextStage()
    {
        nextSceneLoad1P = false;
        nextSceneLoad2P = false;
        remainGears = 5;
        gearItem = 0;
        gauge = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OnLoadSceneInfo(Scene arg, LoadSceneMode arg1)
    {
        if (arg.name.Contains("Vidio"))
        {
            notVidio = false;
        }
        else
        {
            notVidio = true;
        }
        if (arg.name.Contains("TA"))
        {
            timeTAtime = 41;
            timeAttack = GameObject.Find("TimeAttack").GetComponent<Text>();
            gameOverTA_text = GameObject.Find("GameOverTA").GetComponent<Text>();
            gameOverTA_Outline = GameObject.Find("GameOverTA").GetComponent<Outline>();
            reGame_text = GameObject.Find("ReGameText").GetComponent<Text>();
            reGame_Outline = GameObject.Find("ReGameText").GetComponent<Outline>();
            stage_TA = true;
            move = true;

        }
        else
        {
            timeAttack = null;
            stage_TA = false;
        }
    }
}
