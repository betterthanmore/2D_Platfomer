using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public static bool nextScene;       //여러번 눌렀을 때 실행되는 것을 막기 위해
    public Image selectedScreen;        //페이드 아웃되는 이미지

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("SelectedScreen"))         //페이드 아웃되는 이미지가 없다면
        {
            GameObject.Instantiate(selectedScreen, transform);

        }
        nextScene = true;
        selectedScreen = GameObject.Find("SelectedScreen").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectScene()       //스테이지 버튼을 누르면
    {
        if (nextScene)          //여러번 눌렀을 때 실행되는 것을 막기 위해
        {
            nextScene = false;
            StartCoroutine(SelectedSceneLoad()); 
        }
    }
    public IEnumerator SelectedSceneLoad()
    {
        selectedScreen.gameObject.SetActive(true);
        selectedScreen.DOFade(1, 1);
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
