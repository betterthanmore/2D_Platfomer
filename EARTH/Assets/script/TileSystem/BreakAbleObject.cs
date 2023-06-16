using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BreakAbleObject : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public bool isBreak = false;



    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 시작 시 투명한 상태로 설정
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);

        // 페이드인 애니메이션 실행
        spriteRenderer.DOFade(1f, 1f);
    }

    public void DoFadeInOut()
    {
        if(isBreak == false)
        {
            isBreak = true;
            StartCoroutine(CoFadeInOut());
           
        }
    }


    IEnumerator CoFadeInOut()
    {
        spriteRenderer.DOFade(0f, 1f);

        yield return new WaitForSeconds(1.0f);

        spriteRenderer.DOFade(1f, 1f);

        yield return new WaitForSeconds(1.0f);


    }
    public void FadeOut()
    {
        // 페이드아웃 애니메이션 실행
        spriteRenderer.DOFade(0f, 1f);
    }

    public void FadeIn()
    {
        // 페이드아웃 애니메이션 실행
        spriteRenderer.DOFade(1f, 0f);
    }
}
