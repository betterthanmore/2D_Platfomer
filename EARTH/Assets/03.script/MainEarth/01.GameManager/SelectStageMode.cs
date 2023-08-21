using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStageMode : MonoBehaviour
{
   
    public List<Button> chapter1 = new List<Button>();
    public Canvas canvas = null;
    Scene sceneName;
    GameManager GameManager => GameManager.Instance;
    // Start is called before the first frame update
    void Start()
    {
        sceneName.name = "ChapterSelect";
        if (sceneName.name == "ChapterSelect")
        {
            Debug.Log("실행");
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            for (int i = 0; i < canvas.transform.childCount; i++)
            {
                chapter1.Add(canvas.transform.GetChild(i).GetComponentInChildren<Button>());
            }

            for (int i = 0; i < chapter1.Count; i++)
            {
                chapter1[i].interactable = false;
            }

            for (int i = 0; i < GameManager.clearStage; i++)
            {
                chapter1[i].interactable = true;
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Back()
    {
        SceneManager.LoadScene("ChapterSelect");
    }
    public void Chapter1()  //스테이지 고르는 화면으로 변경 해야됨
    {
        SceneManager.LoadScene("Tutorial_Stage1");
    }
    public void Chapter2()
    {
        SceneManager.LoadScene("Tutorial_Stage2");
    }
    public void Chapter3()
    {
        SceneManager.LoadScene("Tutirial_Stage3");
    }

    public void Chap1_Stage1()
    {
        //Stage1 TA_Stage1
        SceneManager.LoadScene("Stage1");
    }
    public void Chap1_Stage2()
    {
        SceneManager.LoadScene("Stage2");
    }
    public void Chap1_Stage3()
    {
        SceneManager.LoadScene("Stage3");
    }
    public void Chap1_Stage4()
    {
        SceneManager.LoadScene("Stage4");
    }
    public void Chap1_Stage5()
    {
        SceneManager.LoadScene("Stage5");
    }
    public void Chap1_Stage6()
    {
        SceneManager.LoadScene("TA_Stage1");
    }
    public void Chap1_Stage7()
    {
        SceneManager.LoadScene("TA_Stage2");
    }
    public void Chap1_Stage8()
    {
        SceneManager.LoadScene("TA_Stage3");
    }
    public void Chap1_Stage9()
    {
        SceneManager.LoadScene("TA_Stage4");
    }
    
}
