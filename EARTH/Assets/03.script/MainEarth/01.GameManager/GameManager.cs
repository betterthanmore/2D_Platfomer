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

    public bool selectStage1 = true;
    public bool selectStage2 = false;
    public bool selectStage3 = false;
    /*public int remainGears = 5;*/
    public int gearItem = 0;
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

    public bool leverPos1 = false;
    public bool leverPos2 = false;


    public bool leverOn1 = false;
    public bool leverOn2 = false;

    public GameObject portal = null;
    /*public Transform portalLever1 = null;
    public Transform portalLever2 = null;*/


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

        /*if (stage)    
        {
            if(leverPos1 = Physics2D.OverlapBox(portalLever1.position, Vector2.one, 0, 256) && !leverOn1)
            {
                if (Input.GetButtonDown("GamePad1_X") || Input.GetKeyDown(KeyCode.Q))
                    leverOn1 = true;
            }
            
            if (leverPos2 = Physics2D.OverlapBox(portalLever2.position, Vector2.one, 0 ,256) && !leverOn2)
            {
                if (Input.GetButtonDown("GamePad1_X") || Input.GetKeyDown(KeyCode.Q))
                    leverOn2 = true;
            }
        }*/

        if(leverOn1 && leverOn2)       
        {
            StartCoroutine("PortalOn");
        }
        
        if (!butttonBPress && reGameButtonDown && nextSceneButtonDown && (!reGame1P || !reGame2P))
        {
            if (Input.GetButtonDown("GamePad2_RB"))
            {
                nextSceneLoad2P = true;
            }
            if (Input.GetButtonDown("GamePad1_RB"))
            {
                nextSceneLoad1P = true;
            }
            if (Input.GetKeyDown(KeyCode.P) || nextSceneLoad1P && nextSceneLoad2P)
            {
                nextSceneButtonDown = false;
                ForceNextStage();
            }
            if (Input.GetButtonDown("GamePad1_X"))
            {
                reGame1P = true;
            }
            if (Input.GetButtonDown("GamePad2_X"))
            {
                reGame2P = true;
            }
            if(reGame1P && reGame2P)
            {
                reGameButtonDown = false;
                reGameStart = true;
            }
        }

        if (reGameStart || Input.GetKeyDown(KeyCode.R))
        {
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
        portal.transform.DOScale(new Vector3(0.4f, 0.4f, 0.4f), 1);
        yield return new WaitForSeconds(1f);
    }
    public void OnLoadSceneInfo(Scene arg, LoadSceneMode arg1)
    {
        leverOn1 = false;
        leverOn2 = false;
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
            /*portalLever1 = GameObject.Find("Lever1").GetComponent<Transform>();
            portalLever2 = GameObject.Find("Lever2").GetComponent<Transform>();*/

        }
        else
        {
            stage = false;
            portal = null;
            reGameButtonDown = false;
            nextSceneButtonDown = false;
            /*portalLever1 = null;
            portalLever2 = null;*/
        }
    }
}
