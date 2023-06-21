using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class StageSelect : MonoBehaviour
{
    public static bool nextScene;       //������ ������ �� ����Ǵ� ���� ���� ����
    public Image selectedScreen;        //���̵� �ƿ��Ǵ� �̹���

    // Start is called before the first frame update
    void Start()
    {
        if (!GameObject.Find("SelectedScreen"))         //���̵� �ƿ��Ǵ� �̹����� ���ٸ�
        {
            GameObject.Instantiate(selectedScreen, transform);

        }
        nextScene = true;
        selectedScreen = GameObject.Find("SelectedScreen").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SelectScene()       //�������� ��ư�� ������
    {
        if (nextScene)          //������ ������ �� ����Ǵ� ���� ���� ����
        {
            nextScene = false;
            StartCoroutine(SelectedSceneLoad()); 
        }
    }
    public IEnumerator SelectedSceneLoad()
    {
        selectedScreen.gameObject.SetActive(true);
        selectedScreen.DOFade(1, 1);
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
