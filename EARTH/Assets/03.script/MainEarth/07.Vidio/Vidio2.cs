using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vidio2 : MonoBehaviour
{

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
        SceneManager.LoadScene(2);

    }
}
