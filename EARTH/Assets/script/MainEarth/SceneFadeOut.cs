using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFadeOut : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color sceneColor;
    private bool sceneColorInit;        //�� �ʱ�ȭ 
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sceneColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        //�̰� ���߿� ���ӸŴ����� �ֱ� �Ǵ� �� �� �����غ���
        if (PlayerMainController.fadeOut == true)
        {
            if(sceneColor.a > 0)
            {
                sceneColor.a -= Time.deltaTime;
            }
            else
            {
                sceneColor.a = 0;
                sceneColorInit = false;
            }
                
        }
    }
}
