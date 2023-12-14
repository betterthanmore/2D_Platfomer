using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimation : MonoBehaviour
{
    Animator an;
    GameObject lever_Ui;
    public float time = 0;
    GameManager GameManager => GameManager.Instance;
    // Start is called before the first frame update
    void Awake()
    {
        an = GetComponent<Animator>();
        lever_Ui = transform.GetChild(0).gameObject;
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

        if(time >= 15)
        {
            lever_Ui.SetActive(false);
        }
        else
        {
            time += Time.deltaTime;
            if(Mathf.Round(time) % 2 == 0)
            {
                lever_Ui.SetActive(false);
            }
            else if(Mathf.Round(time) % 2 == 1)
            {
                lever_Ui.SetActive(true);
            }
        }
    }
}
