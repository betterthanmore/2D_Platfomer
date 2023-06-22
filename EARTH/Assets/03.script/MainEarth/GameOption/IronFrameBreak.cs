using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class IronFrameBreak : MonoBehaviour
{
    public bool isBreak;
    public LayerMask subPlayer;
    private Scrollbar scrollbar;
    GameManager GameManager => GameManager.Instance;
    // Start is called before the first frame update
    void Start()
    {
        scrollbar = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        isBreak = Physics2D.OverlapBox(transform.position, new Vector2(0.5f, 1), 0, subPlayer);
        if (isBreak)
        {
            if (Input.GetButtonDown("GamePad2_LT"))
            {
                Debug.Log("��ư ����");
                StartCoroutine(FadeScreen2());
            }
        }
    }
    public IEnumerator FadeScreen2()
    {
        GameManager.fadeOutscreenBoard.gameObject.SetActive(true);
        GameManager.fadeOutscreenBoard.DOFade(1, 1);
        yield return new WaitForSeconds(1.5f);
        if(gameObject.transform.childCount != 0)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            gameObject.transform.GetChild(0).gameObject.transform.parent = null;
        }
        gameObject.SetActive(false);
        scrollbar.size -= 0.3f;
        GameManager.fadeOutscreenBoard.DOFade(0, 1);
        yield return new WaitForSeconds(1.1f);
        GameManager.fadeOutscreenBoard.gameObject.SetActive(false);
    }
}
