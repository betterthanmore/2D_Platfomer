using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //protected SceneChanger SceneChanger => SceneChanger.Instance;
    public static GameManager Instance { get; private set; }    //�̱��� 
    public GameObject minimumGears;
    public Image fadeOutscreenBoard;           //���̵� �ƿ��Ǵ� �̹���
    public int remainGears = 5;        //���� ������ �������� ���ǿ� ����(�ּ� 5���� �Ծ�ߵǱ� ����)
    public int gearItem;
    private Canvas canvas;
    private bool nextSceneLoad1P = false;
    private bool nextSceneLoad2P = false;
    public bool butttonBPress = false;
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        if (minimumGears != null)
        {
            if (remainGears > 0)
            {
                minimumGears.GetComponent<Text>().text = remainGears + "���� �� ������ ��Ż �̵��� �����մϴ�.";
            }
            if (remainGears <= 0)
            {
                minimumGears.GetComponent<Text>().text = "��Ż �̵��� �����մϴ�.";
            }
        }
        if (fadeOutscreenBoard == null)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            GameObject temp = Instantiate<GameObject>(Resources.Load<GameObject>("fadeOutscreenBoard"), canvas.transform);
            fadeOutscreenBoard = temp.GetComponent<Image>();
            StartCoroutine(FadeScreen());
            minimumGears = GameObject.Find("MinimumGears");
        }
        if (!butttonBPress)
        {
            if (Input.GetButtonDown("GamePad2_X"))
            {
                nextSceneLoad2P = true;
            }
            if (Input.GetButtonDown("GamePad1_X"))
            {
                nextSceneLoad1P = true;
            } 
        }
        if (nextSceneLoad1P && nextSceneLoad2P)
        {
            nextSceneLoad1P = false;
            nextSceneLoad2P = false;
            remainGears = 5;
            gearItem = 0;
            gauge = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Input.GetButtonDown("GamePad1_B") || Input.GetButtonDown("GamePad2_B"))
        {
           /* GameObject temp = gameObject.transform.Find("B_Press").gameObject;*/
            if (!butttonBPress)
            {
                butttonBPress = true;
                /*temp.SetActive(true);*/
                Time.timeScale = 0;
            }
            else
            {
                butttonBPress = false;
                /*temp.SetActive(false);*/
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
}
