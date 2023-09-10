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
    UIManger UIManger => UIManger.uiManger;
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
            if (Input.GetButtonDown("GamePad2_LB") || Input.GetKeyDown(KeyCode.Delete))
            {
                StartCoroutine(FadeScreen2());
            }
        }
    }
    public IEnumerator FadeScreen2()
    {
        GameManager.move = false;
        UIManger.fadeOutscreenBoard.gameObject.SetActive(true);
        UIManger.fadeOutscreenBoard.DOFade(1, 1);
        yield return new WaitForSeconds(1.1f);
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
        if (gameObject.transform.childCount == 2)
        {
            gameObject.transform.GetChild(1).gameObject.SetActive(true);
        }
        scrollbar.size -= 0.1f;
        UIManger.fadeOutscreenBoard.DOFade(0, 1);
        yield return new WaitForSeconds(1.1f);
        UIManger.fadeOutscreenBoard.gameObject.SetActive(false);
        GameManager.move = true;
    }
}
