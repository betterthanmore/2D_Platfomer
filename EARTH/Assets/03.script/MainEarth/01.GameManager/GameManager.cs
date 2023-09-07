using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour
{
    //protected SceneChanger SceneChanger => SceneChanger.Instance;
    public static GameManager Instance { get; private set; }
    UIManger UIManger => UIManger.uiManger;

    public string sceneName;
    public int gearItem = 0;
    public int gearItemInit = 0;
    public bool reGame1P = false;
    public bool reGame2P = false;
    public bool reGameStart = false;
    public bool reGameButtonDown = false;
    public bool buttonBPress = false;
    public float gauge = 1;
    public float gauge_Init = 1;
    public bool stage_TA = false;
    public bool move = true;
    public bool Vidio_N = false;
    public int clearStage = 0;
    public int chapter1Num = 0;
    public int chapter2Num = 0;
    public int chapter3Num = 0;
    public LayerMask playerLayer;

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
        if (!buttonBPress && reGameButtonDown)
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
                if (leverOn1 && leverOn2 && UIManger.minGearTextStart)
                {
                    UIManger.minGearTextStart = false;
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
                else if (UIManger.minGearTextStart)
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
        if (leverPos1 = Physics2D.OverlapBox(portalLever1.position, Vector2.one, 0, playerLayer))
        {
            if (leverPos1.tag == "MainPlayer")
            {
                if (leverOn1 && leverOn2 && UIManger.minGearTextStart)
                {
                    UIManger.minGearTextStart = false;
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
                else if (UIManger.minGearTextStart)
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
                if (leverOn1 && leverOn2 && UIManger.minGearTextStart)
                {
                    UIManger.minGearTextStart = false;
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
                else if (UIManger.minGearTextStart)
                {
                    if (subLeverOn && !leverOn2)
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
        if (leverPos1 = Physics2D.OverlapBox(portalLever1.position, Vector2.one, 0, playerLayer))
        {
            if (leverPos1.tag == "SubPlayer")
            {
                if (leverOn1 && leverOn2 && UIManger.minGearTextStart)
                {
                    UIManger.minGearTextStart = false;
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
                else if (UIManger.minGearTextStart)
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
            else
            {
                leverPos1 = null;
            }
        }
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
            reGame1P = false;
            reGame2P = false;
            reGameButtonDown = true;
            move = true;
            portal = GameObject.Find("Portal_0602");
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
    }
}
