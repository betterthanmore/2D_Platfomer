using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
public class MainScene : MonoBehaviour
{
    public Image img;
    GameManager GameManager => GameManager.Instance;
    // Start is called before the first frame update
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
        GameManager.buttonBPress = false;
        Application.Quit();
    }
    public void StartButton()
    {
        GameManager.buttonBPress = false;
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
}
