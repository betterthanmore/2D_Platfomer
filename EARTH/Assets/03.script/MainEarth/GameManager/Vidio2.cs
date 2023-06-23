using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vidio2 : MonoBehaviour
{
    GameManager GameManager => GameManager.Instance;

    // Start is called before the first frame update
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
        yield return new WaitForSeconds(5);
        GameManager.selectStage1 = false;
        GameManager.selectStage2 = false;
        GameManager.selectStage3 = true;
        SceneManager.LoadScene(2);

    }
}
