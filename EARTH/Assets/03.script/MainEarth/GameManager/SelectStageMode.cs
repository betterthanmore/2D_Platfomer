using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectStageMode : MonoBehaviour
{
    public Button stage1;
    public Button stage2;
    public Button stage3;
    GameManager GameManager => GameManager.Instance;
    // Start is called before the first frame update
    void Start()
    {
        stage1 = GameObject.Find("STAGE-1").GetComponent<Button>();
        stage2 = GameObject.Find("STAGE-2").GetComponent<Button>();
        stage3 = GameObject.Find("STAGE-3").GetComponent<Button>();




    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.selectStage1)
        {
            stage1.interactable = false; ;
        }
        else
        {
            stage1.interactable = true;
        }
        if (!GameManager.selectStage2)
        {
            stage2.interactable = false;
        }
        else
        {
            stage2.interactable = true;
        }
        if (!GameManager.selectStage3)
        {
            stage3.interactable = false;
        }
        else
        {
            stage3.interactable = true;
        }
    }
    public void Stage1()
    {
        if (GameManager.selectStage1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
        }
    }
    public void Stage2()
    {
        if (GameManager.selectStage2)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 11); 
        }
    }
}
