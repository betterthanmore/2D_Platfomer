using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Vidio3 : MonoBehaviour
{

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
        SceneManager.LoadScene(1);

    }
}
