using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //protected SceneChanger SceneChanger => SceneChanger.Instance;
    public GameManager() { }
    public static GameManager Instance { get; private set; }    //싱글톤 

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

}
