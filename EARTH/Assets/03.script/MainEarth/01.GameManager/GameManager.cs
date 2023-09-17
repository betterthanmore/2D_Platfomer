using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    public List<Button> chapter1 = new List<Button>();
    public Canvas canvas = null;
    public List<Button> allButton = new List<Button>();
    public Button back = null;
    public int selectButton = -1;
    public int SelectButton
    {
        get { return selectButton; }

        set
        {
            if (selectButton >= 0)
            {
                if (value > 0)
                {
                    if (value == 1)
                    {
                        if (selectButton == allButton.Count - 1)
                        {
                            selectButton = 0;
                        }
                        else
                        {
                            selectButton += value;
                        }
                    }
                    else if (value == 3)
                    {
                        if (allButton.Count > value)
                        {
                            if(selectButton + value >= allButton.Count)
                            {
                                selectButton -= value * 2;
                            }
                            else
                            {
                                selectButton += value;
                            }
                        }
                        else
                        {
                            selectButton = selectButton + 0;
                            /*selectButton = value - allButton.Count;*/
                        }
                    }
                }
                else if (value < 0)
                {
                    if (value == -1)
                    {
                        if (selectButton == 0)
                        {
                            selectButton = allButton.Count - 1;
                        }
                        else
                        {
                            selectButton += value;
                        }
                    }
                    else if (value == -3)
                    {   
                        if(allButton.Count < Mathf.Abs(value))
                        {
                            selectButton = selectButton + 0;
                        }
                        else if (Mathf.Abs(value) > selectButton)
                        {
                            selectButton += (allButton.Count + value);
                        }
                        else
                        {
                            selectButton += value;
                        }
                    }
                }
                else if(value == 0)
                {
                    selectButton = 0;
                }
            }
            else
            {
                selectButton = 0;
            }
        }
    }
    //protected SceneChanger SceneChanger => SceneChanger.Instance;
    public static GameManager Instance { get; private set; }
    UIManger UIManger => UIManger.uiManger;
    public bool[] nextScene_Press = new bool[2] {false, false};
    public string sceneName;
    public int gearItem = 0;
    public int gearItemInit = 0;
    public bool reGame1P = false;
    public bool reGame2P = false;
    public bool reGameStart = false;
    public bool reGameButtonDown = false;
    public bool buttonBPress = false;
    public bool buttonB_Lock = false;
    public float gauge = 1;
    public float gauge_Init = 1;
    public bool stage_TA = false;
    public bool move = true;
    public bool Vidio_N = false;
    public int clearStage = 0;
    public int[] chapterNum = new int[3] {1, 1, 1};
    public LayerMask playerLayer;

    public Collider2D leverPos1 = null;
    public Collider2D leverPos2 = null;

    public bool mainLeverOn = false;
    public bool subLeverOn = false;
    public bool leverOn1 = false;
    public bool leverOn2 = false;

    public GameObject portal = null;
    public Collider2D portalOnPlayer;
    public bool[] portal_Ready_Player = new bool[2] { false, false };
    public Transform portalLever1 = null;
    public Transform portalLever2 = null;
    /*public bool ironBreak = false;*/
    public GameObject ironBreak = null;

    


    private void Awake()
    {
        Debug.Log("시작");
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
        Debug.Log("시작1");
    }

    private void Update()
    {
        if(portal != null)
        {
            portalOnPlayer = Physics2D.OverlapCircle(portal.transform.position, 1 / 3, playerLayer);
        }
        
        if (!buttonB_Lock && reGameButtonDown)
        {
            if (Input.GetButtonDown("GamePad2_RB"))
            {
                reGame2P = true;
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                reGame2P = true;
                reGame1P = true;
            }
        }
    }
    public void NextScene()
    {
        gauge_Init = gauge;
        gearItemInit = gearItem;
        if (!buttonB_Lock)
        {
            for (int i = 0; i < nextScene_Press.Length; i++)
            {
                nextScene_Press[i] = false;
            }

            if (sceneName.Contains("Chap1"))
            {
                if (chapterNum[0] < 9)
                    chapterNum[0]++;
            }
            else if (sceneName.Contains("Chap2"))
            {
                if (chapterNum[1] < 9)
                    chapterNum[1]++;
            }
            else
            {
                if (chapterNum[2] < 9)
                    chapterNum[2]++;
            }

            if (sceneName.Contains("Last"))
                SceneManager.LoadScene("ChapterSelect");
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            } 
        }
    }
    public void ReGameStart()
    {
        reGameStart = false;
        gearItem = gearItemInit;
        gauge = gauge_Init;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GearNumText(int gear)
    {
        if (UIManger.gearNum != null)
        {
            gearItem += gear;
            UIManger.GearNumText(gearItem);
        }
    }
    public void PortalAction()
    {
        if(portalOnPlayer.tag == "MainPlayer")
        {
            portal_Ready_Player[0] = true;

        }
        else if(portalOnPlayer.tag == "SubPlayer")
        {
            portal_Ready_Player[1] = true;
        }
        
        
        if(portal_Ready_Player[0] && portal_Ready_Player[1])
        {
            NextScene();
        }
    }
    public void PortalOn()
    {
        portal.gameObject.SetActive(true);
        portal.transform.DOScale(new Vector3(0.05f, 0.05f, 0.4f), 1);
    }
    public void MainLever()
    {
        if (leverPos2 = Physics2D.OverlapBox(portalLever2.position, Vector2.one, 0, playerLayer))
        {
            if (leverPos2.tag == "MainPlayer")
            {
                if (leverOn1 && leverOn2)
                {
                    StartCoroutine(UIManger.MinimumGears("포탈은 이미 열렸어 어서가자!"));
                }
                else if (!leverOn2 && !mainLeverOn)
                {
                    mainLeverOn = true;
                    leverOn2 = true;
                    if(leverOn1 && leverOn2)
                    {
                        PortalOn();
                    }
                }
                else
                {
                    if (mainLeverOn && !leverOn2)
                    {
                        StartCoroutine(UIManger.MinimumGears("혼자서 전부 당겨버릴 셈이야?"));
                    }
                    else
                    {
                        StartCoroutine(UIManger.MinimumGears("이미 작동된 레버야!"));
                    }
                }
            }
        }
        else
        {
            leverPos2 = null;
        }
        if (leverPos1 = Physics2D.OverlapBox(portalLever1.position, Vector2.one, 0, playerLayer))
        {
            if (leverPos1.tag == "MainPlayer")
            {
                if (leverOn1 && leverOn2)
                {
                    StartCoroutine(UIManger.MinimumGears("포탈은 이미 열렸어 어서가자!"));
                }
                else if (!leverOn1 && !mainLeverOn)
                {
                    mainLeverOn = true;
                    leverOn1 = true;
                    if (leverOn1 && leverOn2)
                    {
                        PortalOn();
                    }
                }
                else
                {
                    if (mainLeverOn && !leverOn1)
                    {
                        StartCoroutine(UIManger.MinimumGears("혼자서 전부 당겨버릴 셈이야?"));

                    }
                    else
                    {
                        StartCoroutine(UIManger.MinimumGears("이미 작동된 레버야!"));
                    }

                }
            }
            else
            {
                leverPos1 = null;
            }
        }
    }
    public void SubLever()
    {
        if (leverPos2 = Physics2D.OverlapBox(portalLever2.position, Vector2.one, 0, playerLayer))
        {
            if (leverPos2.tag == "SubPlayer")
            {
                if (leverOn1 && leverOn2)
                {
                    StartCoroutine(UIManger.MinimumGears("포탈은 이미 열렸어 어서가자!"));
                }
                else if (!leverOn2 && !subLeverOn)
                {
                    subLeverOn = true;
                    leverOn2 = true;
                    if (leverOn1 && leverOn2)
                    {
                        PortalOn();
                    }
                }
                else
                {
                    if (subLeverOn && !leverOn2)
                    {
                        StartCoroutine(UIManger.MinimumGears("혼자서 전부 당겨버릴 셈이야?"));
                    }
                    else
                    {
                        StartCoroutine(UIManger.MinimumGears("이미 작동된 레버야!"));
                    }
                }
            }
        }
        else
        {
            leverPos2 = null;
        }
        if (leverPos1 = Physics2D.OverlapBox(portalLever1.position, Vector2.one, 0, playerLayer))
        {
            if (leverPos1.tag == "SubPlayer")
            {
                if (leverOn1 && leverOn2)
                {
                    StartCoroutine(UIManger.MinimumGears("포탈은 이미 열렸어 어서가자!"));
                }
                else if (!leverOn1 && !subLeverOn)
                {
                    subLeverOn = true;
                    leverOn1 = true;
                    if (leverOn1 && leverOn2)
                    {
                        PortalOn();
                    }
                }
                else
                {
                    if (subLeverOn && !leverOn1)
                    {
                        StartCoroutine(UIManger.MinimumGears("혼자서 전부 당겨버릴 셈이야?"));

                    }
                    else
                    {
                        StartCoroutine(UIManger.MinimumGears("이미 작동된 레버야!"));
                    }

                }
            }
            else
            {
                leverPos1 = null;
            }
        }
    }
    public void IronBreakStart()
    {
        ironBreak.transform.gameObject.SetActive(false);
        if (ironBreak.transform.childCount == 2)
        {
            ironBreak.transform.GetChild(1).gameObject.SetActive(true);
        }
        SubPlayer.scrollbar.size -= 0.1f;
    }
    public void OnLoadSceneInfo(Scene arg, LoadSceneMode arg1)      //Start함수보다 먼저 실행됨 Awake보단 늦음
    {
        sceneName = SceneManager.GetActiveScene().name;
        leverOn1 = false;
        leverOn2 = false;
        mainLeverOn = false;
        subLeverOn = false;
        buttonB_Lock = true;
        if (arg.name.Contains("Vidio"))
        {
            Vidio_N = false;
        }
        else
        {
            Vidio_N = true;
        }

        if (arg.name.Contains("Stage"))
        {
            reGame1P = false;
            reGame2P = false;
            reGameButtonDown = true;
            move = true;
            portal = GameObject.Find("Portal_0602");
            if (GameObject.FindGameObjectWithTag("IronBreak"))      //나중에 이걸로 프레임 워크 만들면 좋을듯
            {
                ironBreak = GameObject.FindGameObjectWithTag("IronBreak");
            }
            else
            {
                ironBreak = null;
            }
            portal.gameObject.SetActive(false);
            portalLever1 = GameObject.Find("Lever1").GetComponent<Transform>();
            portalLever2 = GameObject.Find("Lever2").GetComponent<Transform>();

        }
        else
        {
            portal = null;
            reGameButtonDown = false;
            portalLever1 = null;
            portalLever2 = null;
        }
        if (arg.name.Contains("Chapter1"))
            clearStage = chapterNum[0];
        else if(arg.name.Contains("Chapter2"))
            clearStage = chapterNum[1];
        else if(arg.name.Contains("Chapter3"))
            clearStage = chapterNum[2];

        if (arg.name.Contains("Chapter"))
        {
            allButton.Clear();
            chapter1.Clear();
            sceneName = SceneManager.GetActiveScene().name;
            if (sceneName.Contains("Mode"))
            {
                canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
                for (int i = 0; i < 9; i++)
                {
                    chapter1.Add(canvas.transform.GetChild(i).GetComponentInChildren<Button>());
                }

                for (int i = 0; i < chapter1.Count; i++)
                {
                    chapter1[i].interactable = false;
                }
                Debug.Log(clearStage);
                for (int i = 0; i < clearStage ; i++)
                {
                    chapter1[i].interactable = true;
                }

            }
            Canvas temp = GameObject.FindObjectOfType<Canvas>();
            for (int i = 0; i < temp.transform.childCount; i++)
            {

                Button parent = temp.transform.GetChild(i).GetComponent<Button>();
                if (parent != null && parent.interactable == true && parent.gameObject.tag == "Untagged")
                    allButton.Add(parent);

            }
            if (GameObject.FindGameObjectWithTag("Back"))
            {
                back = GameObject.FindGameObjectWithTag("Back").GetComponent<Button>();
            }
            else
            {
                back = null;
            }
        }
        else
        {
            canvas = null;
            back = null;

        }


    }
}
