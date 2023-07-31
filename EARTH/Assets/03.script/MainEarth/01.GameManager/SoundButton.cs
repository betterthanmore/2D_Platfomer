using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    public bool Window = false;
    public GameObject SC;
    public bool gksqjs = false;
   
    public void Option()        //옵션 창 눌렀을 때
    {
        if (!Window)
        {
            SoundSC.open = true;
            Window = true;
            SC.gameObject.SetActive(Window);
        }
        if (gksqjs)
        {
            gksqjs = false;
            SoundSC.open = false;
            StopCoroutine(Close());
            StartCoroutine(Close());
        }
        if (!gksqjs)
        {
            Window = false;
        }
        if (Window)
        {
            gksqjs = true;
        }
        

    }

    IEnumerator Close()
    {
        yield return new WaitForSeconds(0.21f);
        Window = false;
        SC.gameObject.SetActive(Window);

    }
}
