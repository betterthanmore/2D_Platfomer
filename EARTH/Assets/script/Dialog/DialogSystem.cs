using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DialogSystem : MonoBehaviour
{
    [SerializeField]
    private Dialog dialogSystem;

    private IEnumerator Start()
    {
        
        yield return new WaitUntil(() => dialogSystem.UpdateDialog(0, true));   //��ٸ��� �Լ� , ���̾�α� �ý����� �Ϸ� �� ������
                                                                                //�μ��� ��� ��ȣ
    }
}