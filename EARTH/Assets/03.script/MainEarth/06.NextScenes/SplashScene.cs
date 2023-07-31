using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SplashScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NSF());
    }

    // Update is called once per frame
    void Update()
    {

    }
    public IEnumerator NSF()
    {
        gameObject.GetComponent<Image>().DOFade(0, 2);
        yield return new WaitForSeconds(2.5f);
        gameObject.GetComponent<Image>().DOFade(1, 2);
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);


    }
}
