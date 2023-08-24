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
    UIManger UIManger => UIManger.uiManger;

    public string sceneName;
    /*public int remainGears = 5;*/
    public int gearItem = 0;
    public int gearItemInit = 0;
    public bool nextSceneLoad1P = false;
    public bool nextSceneLoad2P = false;
    public bool reGame1P = false;
    public bool reGame2P = false;
    public bool reGameStart = false;
    public bool reGameButtonDown = false;
    public bool nextSceneButtonDown = false;
    public bool butttonBPress = false;
    public float gauge = 1;
    public float gauge_Init = 0;
    public bool stage_TA = false;
    public bool donPress_B = false;
    public bool move = true;
    public bool Vidio_N = false;
    public bool keyDown = true;         //키보드로 조작할 경우
    public bool joysticDown = true;     //조이스틱으로 조작할 경우
    public bool stage = false;
    public int clearStage = 0;
    public int chapter1Num = 0;
    public int chapter2Num = 0;
    public int chapter3Num = 0;
    public LayerMask playerLayer;
    public bool timerRestart = false;

    public Collider2D leverPos1 = null;
    public Collider2D leverPos2 = null;

    public bool mainLeverOn = false;
    public bool subLeverOn = false;
    public bool leverOn1 = false;
    public bool leverOn2 = false;

    public GameObject portal = null;
    public Transform portalLever1 = null;
    public Transform portalLever2 = null;


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
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            keyDown = true;
            joysticDown = false;
        }
        else
        {
            keyDown = false;
            joysticDown = true;
        }

        if (stage)
        {
            if (leverPos1 = Physics2D.OverlapBox(portalLever1.position, Vector2.one, 0, playerLayer))
            {
                Debug.Log("감지");
                if (leverPos1.tag == "SubPlayer" && (Input.GetButtonDown("GamePad2_X") || Input.GetKeyDown(KeyCode.O)))
                {
                    if(leverOn1 && leverOn2 && UIManger.minGearTextStart)
                    {
                        UIManger.minGearTextStart = false;
                        StartCoroutine(UIManger.MinimumGears("포탈이 우릴 부르고 있어! 어서 가자!"));
                    }
                    else if (!leverOn1 && !subLeverOn)
                    {
                        subLeverOn = true;
                        leverOn1 = true;
                    }
                    else if (UIManger.minGearTextStart == true)
                    {
                        if (subLeverOn && !leverOn1)
                        {
                            UIManger.minGearTextStart = false;
                            StartCoroutine(UIManger.MinimumGears("혼자서 전부 당겨버릴 셈이야?"));

                        }
                        else
                        {
                            UIManger.minGearTextStart = false;
                            StartCoroutine(UIManger.MinimumGears("이미 작동된 레버야!"));
                        }

                    }
                }
                else if(leverPos1.tag == "MainPlayer" && (Input.GetButtonDown("GamePad1_X") || Input.GetKeyDown(KeyCode.Q)))
                {
                    if (leverOn1 && leverOn2 && UIManger.minGearTextStart)
                    {
                        UIManger.minGearTextStart = false;
                        StartCoroutine(UIManger.MinimumGears("포탈이 우릴 부르고 있어! 어서 가자!"));
                    }
                    else if (!leverOn1 && !mainLeverOn)
                    {
                        mainLeverOn = true;
                        leverOn1 = true;
                    }
                    else if (UIManger.minGearTextStart )
                    {
                        if (mainLeverOn && !leverOn1)
                        {
                            UIManger.minGearTextStart = false;
                            StartCoroutine(UIManger.MinimumGears("혼자서 전부 당겨버릴 셈이야?"));

                        }
                        else
                        {
                            UIManger.minGearTextStart = false;
                            StartCoroutine(UIManger.MinimumGears("이미 작동된 레버야!"));
                        }

                    }
                }
            }
            else
            {
                leverPos1 = null;
            }

            if (leverPos2 = Physics2D.OverlapBox(portalLever2.position, Vector2.one, 0, playerLayer))
            {
                Debug.Log("감지");
                if (leverPos2.tag == "SubPlayer" && (Input.GetButtonDown("GamePad2_X") || Input.GetKeyDown(KeyCode.O)))
                {
                    if (leverOn1 && leverOn2 && UIManger.minGearTextStart)
                    {
                        UIManger.minGearTextStart = false;
                        StartCoroutine(UIManger.MinimumGears("포탈이 우릴 부르고 있어! 어서 가자!"));
                    }
                    else if (!leverOn2 && !subLeverOn)
                    {
                        subLeverOn = true;
                        leverOn2 = true;
                    }
                    else if(UIManger.minGearTextStart)
                    {
                        if (subLeverOn && !leverOn2 && UIManger.minGearTextStart)
                        {
                            UIManger.minGearTextStart = false;
                            StartCoroutine(UIManger.MinimumGears("혼자서 전부 당겨버릴 셈이야?"));

                        }
                        else
                        {
                            UIManger.minGearTextStart = false;
                            StartCoroutine(UIManger.MinimumGears("이미 작동된 레버야!"));
                        }
                        
                    }
                    
                }
                else if (leverPos2.tag == "MainPlayer" && (Input.GetButtonDown("GamePad1_X") || Input.GetKeyDown(KeyCode.Q)))
                {
                    if (leverOn1 && leverOn2 && UIManger.minGearTextStart)
                    {
                        UIManger.minGearTextStart = false;
                        StartCoroutine(UIManger.MinimumGears("포탈이 우릴 부르고 있어! 어서 가자!"));
                    }
                    else if (!leverOn2 && !mainLeverOn)
                    {
                        mainLeverOn = true;
                        leverOn2 = true;
                    }
                    else if(UIManger.minGearTextStart)
                    {
                        if (mainLeverOn && !leverOn2)
                        {
                            UIManger.minGearTextStart = false;
                            StartCoroutine(UIManger.MinimumGears("혼자서 전부 당겨버릴 셈이야?"));
                        }
                        else
                        {
                            UIManger.minGearTextStart = false;
                            StartCoroutine(UIManger.MinimumGears("이미 작동된 레버야!"));
                        }
                        
                    }
                }
            }
            else
            {
                leverPos2 = null;
            }
        }

        if (leverOn1 && leverOn2)       
        {
            StartCoroutine("PortalOn");
        }
        
        if ((!butttonBPress && reGameButtonDown && nextSceneButtonDown && (!reGame1P || !reGame2P)) || timerRestart)
        {
            /*if (Input.GetButtonDown("GamePad2_RB"))
            {
                nextSceneLoad2P = true;
            }
            if (Input.GetButtonDown("GamePad1_RB"))
            {
                nextSceneLoad1P = true;
            }*/
            if (Input.GetKeyDown(KeyCode.P) || nextSceneLoad1P && nextSceneLoad2P)
            {
                nextSceneButtonDown = false;
                ForceNextStage();
            }
            if (Input.GetButtonDown("GamePad1_LB"))
            {
                reGame1P = true;
            }
            if (Input.GetButtonDown("GamePad2_LB"))
            {
                reGame2P = true;
            }
            if(reGame1P && reGame2P)
            {
                reGameButtonDown = false;
                reGameStart = true;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                reGame2P = true;
                reGame1P = true;
            }
        }

        if (reGameStart)
        {
            timerRestart = false;
            reGameStart = false;
            ReGameStart();
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
    public void ReGameStart()
    {
        gearItem = gearItemInit;
        gauge = gauge_Init;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ForceNextStage()
    {
        nextSceneLoad1P = false;
        nextSceneLoad2P = false;
        /*remainGears = 5;*/
        gearItem = 0;
        gauge = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    IEnumerator PortalOn()
    {
        portal.gameObject.SetActive(true);
        portal.transform.DOScale(new Vector3(0.05f, 0.05f, 0.4f), 1);
        yield return new WaitForSeconds(1f);
    }
    public void OnLoadSceneInfo(Scene arg, LoadSceneMode arg1)
    {
        sceneName = SceneManager.GetActiveScene().name;
        leverOn1 = false;
        leverOn2 = false;
        mainLeverOn = false;
        subLeverOn = false;
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
            nextSceneLoad1P = false;
            nextSceneLoad2P = false;
            nextSceneButtonDown = true;
            reGame1P = false;
            reGame2P = false;
            reGameButtonDown = true;
            move = true;
            stage = true;
            portal = GameObject.Find("Portal_0602");
            portal.gameObject.SetActive(false);
            portalLever1 = GameObject.Find("Lever1").GetComponent<Transform>();
            portalLever2 = GameObject.Find("Lever2").GetComponent<Transform>();

        }
        else
        {
            stage = false;
            portal = null;
            reGameButtonDown = false;
            nextSceneButtonDown = false;
            portalLever1 = null;
            portalLever2 = null;
        }
    }
}
