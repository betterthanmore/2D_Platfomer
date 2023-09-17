using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class SelectStageMode : MonoBehaviour
{
    

    
    public string sceneName;
    GameManager GameManager => GameManager.Instance;
    UIManger UIManger => UIManger.uiManger;
    // Start is called before the first frame update
    
    
    public void ButtonSelect(InputAction.CallbackContext input)
    {
        Debug.Log(input.control.name);
        if (input.started && input.control.device.name == "XInputControllerWindows" && !GameManager.buttonB_Lock && !GameManager.buttonBPress)
        {
            if (input.control.name == "left")
            {
                GameManager.SelectButton = -1;
            }
            else if(input.control.name == "right")
            {
                GameManager.SelectButton = 1;
            }
            for (int i = 0; i < GameManager.allButton.Count; i++)
            {
                if (GameManager.allButton[i] == GameManager.allButton[GameManager.SelectButton])
                    GameManager.allButton[i].animator.SetTrigger("Highlighted");
                else
                    GameManager.allButton[i].animator.SetTrigger("Normal");
            }
        }
    }
    public void BackButton(InputAction.CallbackContext input)
    {
        if (input.control.device.name == "XInputControllerWindows" && input.started && !GameManager.buttonB_Lock && !GameManager.buttonBPress)
        {
            GameManager.back.onClick?.Invoke();
        }
    }
    public void ButtonDownUP(InputAction.CallbackContext input)
    {
        if(input.started && input.control.device.name == "XInputControllerWindows" && !GameManager.buttonB_Lock && !GameManager.buttonBPress)
        {
            if (input.control.name == "up")
                GameManager.SelectButton = -3;
            else if(input.control.name == "down")
            {
                if(GameManager.selectButton == 6)
                {
                    GameManager.selectButton = 0;
                }
                else
                {
                    GameManager.SelectButton = 3;
                }
            }

            for (int i = 0; i < GameManager.allButton.Count; i++)
            {
                if (GameManager.allButton[i] == GameManager.allButton[GameManager.SelectButton])
                    GameManager.allButton[i].animator.SetTrigger("Highlighted");
                else
                    GameManager.allButton[i].animator.SetTrigger("Normal");
            }
        }
    }
    
    public void ButtonPress(InputAction.CallbackContext input)
    {
        if (input.control.device.name == "XInputControllerWindows" && input.started && !GameManager.buttonB_Lock && !GameManager.buttonBPress)
        {
            GameManager.allButton[GameManager.selectButton].onClick?.Invoke();
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
