using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    GameManager GameManager => GameManager.Instance;
    UIManger UIManger => UIManger.uiManger;
    public Text text;
    public Text text2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        text.text = "����" + GameManager.playerSetting[0].name;
        text2.text = "����" + GameManager.playerSetting[1].name;

    }
}
