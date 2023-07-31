using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Vidio3 : MonoBehaviour
{
    GameManager GameManager => GameManager.Instance;

    void Start()
    {
        StartCoroutine(StageVidio2());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator StageVidio2()
    {
        yield return new WaitForSeconds(26);
        GameManager.selectStage1 = true;
        GameManager.selectStage2 = false;
        GameManager.selectStage3 = false;
        SceneManager.LoadScene(1);

    }
}
