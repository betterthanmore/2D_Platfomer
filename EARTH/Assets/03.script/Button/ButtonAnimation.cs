using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    Animator an;
    GameManager GameManager => GameManager.Instance;
    // Start is called before the first frame update
    void Awake()
    {
        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.name == "Lever1" && GameManager.leverOn1)
        {
            an.SetBool("LeverOn", true);
        }
        else if(gameObject.name == "Lever2" && GameManager.leverOn2)
        {
            an.SetBool("LeverOn", true);
        }
    }
}
