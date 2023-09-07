using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManger : MonoBehaviour
{
    public static UIManger uiManger { get; private set; }
    GameManager GameManager => GameManager.Instance;
    public Text minGearText;
    public Outline miniGearText_Outline;
    public Text time_TA_Text;
    private Text gameOverTA_Text;
    private Outline gameOverTA_Outline;
    private Text reGame_text;
    private Outline reGame_Outline;
    public Text gearNum;
    public float timeTAtime = 41;
    public Canvas canvas;
    public Image fadeOutscreenBoard;
    public GameObject pauseScreen;
    public bool minGearTextStart = true;


    // Start is called before the first frame update
    private void Awake()
    {
        if (uiManger)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            transform.parent = null;
            uiManger = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += UIOnLoadSceneInfo;
        }

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.stage_TA)
        {
            TimeATTACK();
        }
        if (GameManager.reGameStart)
        {
            StartCoroutine(ReGameTxet());
        }
        if(pauseScreen != null)
        {
            if (GameManager.buttonBPress)
                pauseScreen.SetActive(true);
            else
                pauseScreen.SetActive(false);
        }
        
    }
    public IEnumerator FadeScreen()
    {
        fadeOutscreenBoard = GameObject.Find("FadeOutscreenBoard").GetComponent<Image>();
        GameManager.buttonBPress = false;
        yield return new WaitForSeconds(0.5f);
        fadeOutscreenBoard.DOFade(0, 1);
        yield return new WaitForSeconds(1.1f);
        fadeOutscreenBoard.gameObject.SetActive(false);
        if (GameManager.Vidio_N)
        {
            GameManager.buttonBPress = true;
        }
    }
    public IEnumerator MinimumGears(string text)
    {
        minGearTextStart = false;
        minGearText.text = text;
        miniGearText_Outline.DOFade(0f, 0f);
        minGearText.DOFade(0, 0);
        minGearText.DOFade(1f, 1f);
        miniGearText_Outline.DOFade(1f, 1f);
        yield return new WaitForSeconds(1.5f);
        minGearText.DOFade(0f, 1f);
        miniGearText_Outline.DOFade(0f, 1f);
        yield return new WaitForSeconds(1.01f);
        minGearTextStart = true;
    }

    public void TimeATTACK()
    {

        if (timeTAtime > 0)
        {
            time_TA_Text.text = ((int)Mathf.Clamp(timeTAtime -= Time.deltaTime, 0, timeTAtime)).ToString();
        }
        else if (timeTAtime <= 0)
        {
            GameManager.stage_TA = false;
            gameOverTA_Text.DOFade(1, 1);
            gameOverTA_Outline.DOFade(1, 1);
            gameOverTA_Text.gameObject.transform.DOLocalMove(Vector2.zero, 1);
            GameManager.reGameStart = true;
            StartCoroutine(ReGameTxet());
        }

    }
    public void GearNumText(int num)
    {
        if (gearNum != null)
        {
            gearNum.text = "x" + num;
        }
    }
    
    public IEnumerator ReGameTxet()
    {
        GameManager.buttonBPress = false;
        GameManager.move = false;
        yield return new WaitForSeconds(1);
        reGame_text.DOFade(1, 1);
        reGame_Outline.DOFade(1, 1);
        reGame_text.text = " 양쪽 플레이어 모두 RB버튼을 누르면 게임이 재시작합니다.";
            
    }
    public void UIOnLoadSceneInfo(Scene arg, LoadSceneMode arg1)
    {

        if (!arg.name.Contains("Main"))
        {
            if (!arg.name.Contains("Vidio"))
            {
                if (arg.name.Contains("Stage"))
                {
                    canvas = Instantiate<Canvas>(Resources.Load<Canvas>("Canvas"), null);
                    reGame_text = GameObject.Find("ReGameText").GetComponent<Text>();
                    reGame_Outline = GameObject.Find("ReGameText").GetComponent<Outline>();
                    minGearText = GameObject.Find("MinimumGears").GetComponent<Text>();
                    gearNum = GameObject.Find("GearNum").GetComponent<Text>();
                    if (arg.name.Contains("TA"))
                    {
                        timeTAtime = 41;
                        time_TA_Text = GameObject.Find("TimeAttack").GetComponent<Text>();
                        gameOverTA_Text = GameObject.Find("GameOverTA").GetComponent<Text>();
                        gameOverTA_Outline = GameObject.Find("GameOverTA").GetComponent<Outline>();
                        GameManager.stage_TA = true;

                    }
                    else
                    {
                        time_TA_Text = null;
                        gameOverTA_Text = null;
                        gameOverTA_Outline = null;
                        GameManager.stage_TA = false;
                    }
                }
                pauseScreen = GameObject.Find("B_Press");
                pauseScreen.gameObject.SetActive(false);
            }
            else
            {
                canvas = null;
                reGame_text = null;
                reGame_Outline = null;
                minGearText = null;
                pauseScreen = null;

                gearNum = null;
                time_TA_Text = null;
                gameOverTA_Text = null;
                gameOverTA_Outline = null;
                GameManager.stage_TA = false;
            } 
        }
        else
        {
            canvas = null;
            reGame_text = null;
            reGame_Outline = null;
            minGearText = null;
            pauseScreen = null;

            gearNum = null;
            time_TA_Text = null;
            gameOverTA_Text = null;
            gameOverTA_Outline = null;
            GameManager.stage_TA = false;
        }
        
        StartCoroutine(FadeScreen());
    }
}
