using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class BreakAbleObject : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;

    public bool isBreak = true;



    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ���� �� ������ ���·� ����
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0f);
    }

    public void DoFadeInOut()
    {
        if(isBreak == false)
        {
            Debug.Log("false ����");
            isBreak = true;
            StartCoroutine(CoFadeInOut());
           
        }
    }


    IEnumerator CoFadeInOut()
    {
        spriteRenderer.DOFade(1f, 1f);

        yield return new WaitForSeconds(1.5f);

        spriteRenderer.DOFade(0f, 1f);
        yield return new WaitForSeconds(1.0f);
        isBreak = false;
        Debug.Log("false�� �ٲ�");

    }
}
