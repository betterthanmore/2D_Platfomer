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
    public static GameManager Instance { get; private set; }    //�̱��� 
    public GameObject minimumGears;
    public Image fadeOutscreenBoard;           //���̵� �ƿ��Ǵ� �̹���
    public static bool nextScene;       //������ ������ �� ����Ǵ� ���� ���� ����
    public int mixGears = 5;        //���� ������ �������� ���ǿ� ����(�ּ� 5���� �Ծ�ߵǱ� ����)
    public int gearItem;
    private Canvas canvas;
    private Image blackBoard;
    private bool nextSceneLoad1P = false;
    private bool nextSceneLoad2P = false;


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
            minimumGears.GetComponent<Text>().text = mixGears + "���� �� ������ ��Ż �̵��� �����մϴ�.";
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
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        /*if(blackBoard == null)
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            GameObject temp2 = Instantiate<GameObject>(Resources.Load<GameObject>("BlackBoard"), canvas.transform);
            blackBoard = temp2.GetComponent<Image>();

        }*/
    }
    public IEnumerator MinimumGears()
    {
        minimumGears.GetComponent<Text>().DOFade(1f, 1f);
        yield return new WaitForSeconds(1.5f);
        minimumGears.GetComponent<Text>().DOFade(0f, 1f);
        yield return new WaitForSeconds(1.01f);
        

    }
    public void SelectScene()       //�������� ��ư�� ������
    {
        if (nextScene)          //������ ������ �� ����Ǵ� ���� ���� ����
        {
            Debug.Log("����");
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
    
    /*public IEnumerator BlackScreen()
    {
        blackBoard.DOFade
    }*/
}
