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
    /*public GameObject player1;
    public GameObject player2;
    public Transform playerPos1;
    public Transform PlayerPos2;*/
    public GameObject minimumGears;
    private bool minimumbool = true;
    public Image fadeOutscreenBoard;           //���̵� �ƿ��Ǵ� �̹���

    public static bool nextScene;       //������ ������ �� ����Ǵ� ���� ���� ����

    public int mixGears = 5;        //���� ������ �������� ���ǿ� ����(�ּ� 5���� �Ծ�ߵǱ� ����)
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
        Debug.Log("��ŸƮ ����");
        
        


    }
    private void Update()
    {
        if (minimumGears != null)
        {
            minimumGears.GetComponent<Text>().text = mixGears + "���� �� ������ ��Ż �̵��� �����մϴ�.";
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
    public void SelectScene()       //�������� ��ư�� ������
    {
        if (nextScene)          //������ ������ �� ����Ǵ� ���� ���� ����
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
        if (!GameObject.Find("FadeOutscreenBoard"))         //���̵� �ƿ��Ǵ� �̹����� ���ٸ�
        {

        }
        nextScene = true;
        fadeOutscreenBoard = GameObject.Find("FadeOutscreenBoard").GetComponent<Image>();
        StartCoroutine(FadeScreen());
        Debug.Log("����");
        minimumGears = GameObject.Find("MinimumGears");
    }

}
