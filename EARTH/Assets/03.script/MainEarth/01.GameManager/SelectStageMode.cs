using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStageMode : MonoBehaviour
{

    public List<Button> chapter1 = new List<Button>();
    public Canvas canvas = null;
    string sceneName;
    GameManager GameManager => GameManager.Instance;
    // Start is called before the first frame update
    void Start()
    {

        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Contains("Mode"))
        {
            canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            for (int i = 0; i < 8; i++)
            {
                chapter1.Add(canvas.transform.GetChild(i).GetComponentInChildren<Button>());
            }

            for (int i = 0; i < chapter1.Count; i++)
            {
                chapter1[i].interactable = false;
            }

            for (int i = 0; i < GameManager.clearStage + 1; i++)
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
        SceneManager.LoadScene("Chapter1_Mode");
    }
    public void Chapter2()
    {
        SceneManager.LoadScene("Chapter2_Mode");
    }
    public void Chapter3()
    {
        SceneManager.LoadScene("Chapter3_Mode");
    }

    public void Chap1_Stage1()
    {
        //Stage1 TA_Stage1
        SceneManager.LoadScene("Chap1_Tutorial_Stage1");
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


    public void Chap2_Stage1()
    {
        //Stage1 TA_Stage1
        SceneManager.LoadScene("Chap2_Tutorial_Stage2");
    }
    public void Chap2_Stage2()
    {
        SceneManager.LoadScene("Stage6");
    }
    public void Chap2_Stage3()
    {
        SceneManager.LoadScene("Stage7");
    }
    public void Chap2_Stage4()
    {
        SceneManager.LoadScene("Stage8");
    }
    public void Chap2_Stage5()
    {
        SceneManager.LoadScene("Stage9");
    }
    public void Chap2_Stage6()
    {
        SceneManager.LoadScene("TA_Stage5");
    }
    public void Chap2_Stage7()
    {
        SceneManager.LoadScene("TA_Stage6");
    }
    public void Chap2_Stage8()
    {
        SceneManager.LoadScene("TA_Stage7");
    }
    public void Chap2_Stage9()
    {
        SceneManager.LoadScene("TA_Stage8");
    }
    public void Chap3_Stage1()
    {
        //Stage1 TA_Stage1
        SceneManager.LoadScene("Chap3_Tutorial_Stage3");
    }
    public void Chap3_Stage2()
    {
        SceneManager.LoadScene("Stage10");
    }
    public void Chap3_Stage3()
    {
        SceneManager.LoadScene("Stage11");
    }
    public void Chap3_Stage4()
    {
        SceneManager.LoadScene("Stage12");
    }
    public void Chap3_Stage5()
    {
        SceneManager.LoadScene("Stage13");
    }
    public void Chap3_Stage6()
    {
        SceneManager.LoadScene("TA_Stage9");
    }
    public void Chap3_Stage7()
    {
        SceneManager.LoadScene("TA_Stage10");
    }
    public void Chap3_Stage8()
    {
        SceneManager.LoadScene("TA_Stage11");
    }
    public void Chap3_Stage9()
    {
        SceneManager.LoadScene("TA_Stage12");
    }
}
