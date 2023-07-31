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
    
    public int remainGears = 5;       
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
    public bool selectStage1 = true;
    public bool selectStage2 = false;
    public bool selectStage3 = false;
    public bool stage_TA = false;
    public bool donPress_B = false;
    public bool move = true;
    public bool Vidio_N = false;
    public bool keyDown = true;         //키보드로 조작할 경우
    public bool joysticDown = true;     //조이스틱으로 조작할 경우

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
                //여기에 게임 매니저에서 불값 하나를 더 만들어 그게 트루일 때 씬이 넘어갈 수 있도록 변경
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
            Vidio_N = false;
        }
        else if (arg.name.Contains("Stage"))
        {
            nextSceneLoad1P = false;
            nextSceneLoad2P = false;
            nextSceneButtonDown = true;
            reGame1P = false;
            reGame2P = false;
            reGameButtonDown = true;
            move = true;
        }
        else if(!arg.name.Contains("Stage"))
        {
            reGameButtonDown = false;
            nextSceneButtonDown = false;
        }
        else
        {
            Vidio_N = true;
        }
        
    }
}
