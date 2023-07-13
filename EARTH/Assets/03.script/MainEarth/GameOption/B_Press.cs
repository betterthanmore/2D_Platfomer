using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_Press : MonoBehaviour
{
    private GameManager GameManager => GameManager.Instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.butttonBPress)
        {
            gameObject.transform.Find("B_Press").gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.Find("B_Press").gameObject.SetActive(false);
        }
    }
}
