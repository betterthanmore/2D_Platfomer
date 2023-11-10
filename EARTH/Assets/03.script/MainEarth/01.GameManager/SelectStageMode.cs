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
    public void ButtonDownUP(InputAction.CallbackContext input)//��ư�� Ȱ��ȭ �����ִ� ���� ���� ��ư���� ��ȣ�� ��Ÿ����
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
                if (GameManager.allButton[i] == GameManager.allButton[GameManager.SelectButton])            //��� ��ư���� �˻��ؼ� ���� ������ ��ư�� Ȱ��ȭ �����ִ� ����
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
    #region é�� ���� ��
    public void Chapter1()  //�������� ���� ȭ������ ���� �ؾߵ�
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
    #endregion
    #region é�� 1 ��������
    public void Chap1_Stage1()
    {
        //Stage1 TA_Stage1
        SceneManager.LoadScene("Chap1_Tutorial_Stage1");
    }
    public void Chap1_Stage2()
    {
        SceneManager.LoadScene("Chap1_Stage2");
    }
    public void Chap1_Stage3()
    {
        SceneManager.LoadScene("Chap1_Stage3");
    }
    public void Chap1_Stage4()
    {
        SceneManager.LoadScene("Chap1_Stage4");
    }
    public void Chap1_Stage5()
    {
        SceneManager.LoadScene("Chap1_Stage5");
    }
    public void Chap1_Stage6()
    {
        SceneManager.LoadScene("Chap1_TA_Stage1");
    }
    public void Chap1_Stage7()
    {
        SceneManager.LoadScene("Chap1_TA_Stage2");
    }
    public void Chap1_Stage8()
    {
        SceneManager.LoadScene("Chap1_TA_Stage3");
    }
    public void Chap1_Stage9()
    {
        SceneManager.LoadScene("Chap1_TA_Stage4(Chap1_Last)");
    }
    #endregion

    #region é�� 2 ��������
    public void Chap2_Stage1()
    {
        //Stage1 TA_Stage1
        SceneManager.LoadScene("Chap2_Tutorial_Stage2");
    }
    public void Chap2_Stage2()
    {
        SceneManager.LoadScene("Chap2_Stage7");
    }
    public void Chap2_Stage3()
    {
        SceneManager.LoadScene("Chap2_Stage8");
    }
    public void Chap2_Stage4()
    {
        SceneManager.LoadScene("Chap2_Stage9");
    }
    public void Chap2_Stage5()
    {
        SceneManager.LoadScene("Chap2_TA_Stage5");
    }
    public void Chap2_Stage6()
    {
        SceneManager.LoadScene("Chap2_TA_Stage6");
    }
    public void Chap2_Stage7()
    {
        SceneManager.LoadScene("Chap2_TA_Stage7");
    }
    public void Chap2_Stage8()
    {
        SceneManager.LoadScene("Chap2_TA_Stage8");
    }
    public void Chap2_Stage9()
    {
        SceneManager.LoadScene("Chap2_TA_Stage9(Chap2_Last)");
    }
    #endregion
    #region é�� 3 ��������
    public void Chap3_Stage1()
    {
        //Stage1 TA_Stage1
        SceneManager.LoadScene("Chap3_Tutorial_Stage3");
    }
    public void Chap3_Stage2()
    {
        SceneManager.LoadScene("Chap3_Stage11");
    }
    public void Chap3_Stage3()
    {
        SceneManager.LoadScene("Chap3_Stage12");
    }
    public void Chap3_Stage4()
    {
        SceneManager.LoadScene("Chap3_Stage13");
    }
    public void Chap3_Stage5()
    {
        SceneManager.LoadScene("Chap3_Stage14");
    }
    public void Chap3_Stage6()
    {
        SceneManager.LoadScene("Chap3_TA_Stage10");
    }
    public void Chap3_Stage7()
    {
        SceneManager.LoadScene("Chap3_TA_Stage11");
    }
    public void Chap3_Stage8()
    {
        SceneManager.LoadScene("Chap3_TA_Stage12");
    }
    public void Chap3_Stage9()
    {
        SceneManager.LoadScene("Chap3_TA_Stage13(Chap3_Last)");
    }
    #endregion
}
