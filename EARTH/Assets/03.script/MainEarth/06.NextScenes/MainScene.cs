using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class MainScene : MonoBehaviour
{
    public List<Button> allButton = new List<Button>();
    public int selectButton = -1;
    public int SelectButton 
    {
        get {return selectButton; }

        set
        {
            if (selectButton == allButton.Count - 1)
            {
                selectButton = 0;
            }
            else
            {
                selectButton += value;
            }
        }
    }
    public Image img;
    GameManager GameManager => GameManager.Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        /*Button[] tempButton = GameObject.FindObjectsOfType<Button>();
        for (int i = 0; i < tempButton.Length; i++)
        {
            allButton.Add(tempButton[i]);
        }*/
        Canvas temp = GameObject.FindObjectOfType<Canvas>();
        for (int i = 0; i < temp.transform.childCount; i++)
        {
            Button parent = temp.transform.GetChild(i).GetComponent<Button>();
            if (parent != null)
                allButton.Add(parent);
        }
    }
    void Start()
    {
        StartCoroutine(MainSceneLoad());
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void StartButton()
    {
        GameManager.buttonB_Lock = true;
        StartCoroutine(StartButtonNextScene());
    }
    public IEnumerator StartButtonNextScene()
    {
        img.gameObject.SetActive(true);
        img.DOFade(1, 1.5f);
        yield return new WaitForSeconds(1.6f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public IEnumerator MainSceneLoad()
    {
        img.DOFade(0, 1.5f);
        yield return new WaitForSeconds(1.6f);
        img.gameObject.SetActive(false);
    }
    public void ButtonSelect(InputAction.CallbackContext input)
    {
        Debug.Log(input.control.device.name);
        if (input.started && input.control.device.name == "XInputControllerWindows")
        {
            SelectButton = 1;

            for (int i = 0; i < allButton.Count; i++)
            {
                if(allButton[i] == allButton[SelectButton])
                    allButton[i].animator.SetTrigger("Highlighted");
                else
                    allButton[i].animator.SetTrigger("Normal");
            }
        }
    }
    public void ButtonPress(InputAction.CallbackContext input)
    {
        if (input.control.device.name == "XInputControllerWindows" && input.started)
        {
            allButton[selectButton].onClick?.Invoke();
        }
    }
}
