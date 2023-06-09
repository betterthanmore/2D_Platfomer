using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Material testM;
    private float timeM;
    // Start is called before the first frame update
    void Start()
    {
        testM.color = new Color(0, 0, 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if(timeM <= 3)
        {
            timeM += Time.fixedDeltaTime / 3;
        }
        testM.color = new Color(0, 0, 0, 1 * timeM / 3);
    }
}
