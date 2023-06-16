using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFadeOut : MonoBehaviour
{
    private SpriteRenderer sr;
    private Color sceneColor;
    private bool sceneColorInit;        //색 초기화 
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sceneColor = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        //이건 나중에 게임매니저에 넣기 또는 좀 더 생각해보기
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
