using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    //protected SceneChanger SceneChanger => SceneChanger.Instance;
    public GameManager() { }
    public static GameManager Instance { get; private set; }    //싱글톤 
    public GameObject player1;
    public GameObject player2;
    public Transform playerPos1;
    public Transform PlayerPos2;
    private GameObject minimumGears;
    private bool minimumbool = true;

    public int mixGears = 5;        //다음 맵으로 가기위한 조건용 변수(최소 5개를 먹어야되기 때문)
    public int gearItem;

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
        minimumGears = GameObject.Find("MinimumGears");
        
    }
    private void Update()
    {
        minimumGears.GetComponent<Text>().text = mixGears + "개만 더 모으면 포탈 이동이 가능합니다.";

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
    
}
