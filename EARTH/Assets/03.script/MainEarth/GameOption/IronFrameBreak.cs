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
    public float sizeX;
    public float sizeY;
    GameManager GameManager => GameManager.Instance;
    // Start is called before the first frame update
    void Start()
    {
        scrollbar = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        isBreak = Physics2D.OverlapBox(transform.position, new Vector2(sizeX - 0.47f, sizeY), 0, subPlayer);
        if (isBreak)
        {
            if (Input.GetButtonDown("GamePad2_LT"))
            {
                Debug.Log("버튼 반응");
                StartCoroutine(FadeScreen2());
            }
        }
    }
    public IEnumerator FadeScreen2()
    {
        GameManager.move = false;
        GameManager.fadeOutscreenBoard.gameObject.SetActive(true);
        GameManager.fadeOutscreenBoard.DOFade(1, 1);
        yield return new WaitForSeconds(1.5f);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        if (gameObject.transform.childCount == 2)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        scrollbar.size -= 0.1f;
        GameManager.fadeOutscreenBoard.DOFade(0, 1);
        yield return new WaitForSeconds(1.1f);
        GameManager.fadeOutscreenBoard.gameObject.SetActive(false);
        GameManager.move = true;
        Debug.Log("움직일수있어");
    }
}
