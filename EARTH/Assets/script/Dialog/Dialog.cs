using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;                           //Arry ���� ����� ����ϱ� ���� �߰�
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private SpeakerUI[] speakers;                                       //��ȭ�� �����ϴ� ĳ���͵��� UI �迭
    [SerializeField]
    private DialogData[] dialogs;                                       //���� �б��� ��� ��� �迭
    [SerializeField]
    private bool DialogInit = true;                                     //�ڵ� ���� ����
    [SerializeField]
    private bool dialogsDB = false;                                     //DB�� ���� �д� �� ����

    public int currentDialogIndex = -1;                                 //���� ��� ����
    public int currentSpeakerIndex = 0;                                 //���� ���� �ϴ� ȭ���� Speakers �迭 ����
    public float typingSpeed = 0.1f;                                    //�ؽ�Ʈ Ÿ���� ȿ���� ��� �ӵ�
    public bool isTypingEffect = false;                                 //�ؽ�Ʈ Ÿ���� ȿ���� ��������� �Ǵ�.

    public List<string> sceneNameList = new List<string>();
    //���⼭���� �Ʒ� �Ұ��� ���߿� ����
    public bool sceneLoad1;
    public bool sceneLoad2;
    public bool sceneLoad3;

    public Entity_Dialog entity_Dialogue;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneName;
        SetAllClose();
        
        //���� �ٱ� if�� ���߿� ����
        if (sceneLoad1)
        {

            if (dialogsDB)
            {
                Array.Clear(dialogs, 0, dialogs.Length);
                Array.Resize(ref dialogs, entity_Dialogue.sheets[0].list.Count);

                int ArrayCursor = 0;
                foreach (Entity_Dialog.Param param in entity_Dialogue.sheets[0].list)
                {
                    dialogs[ArrayCursor].index = param.index;
                    dialogs[ArrayCursor].speakerUIindex = param.speakerUIindex;
                    dialogs[ArrayCursor].name = param.name;
                    dialogs[ArrayCursor].dialogue = param.dialogue;
                    dialogs[ArrayCursor].characterPath = param.characterPath;
                    dialogs[ArrayCursor].nextindex = param.nextindex;
                    ArrayCursor += 1;
                }
            } 
        }
        if (sceneLoad2)
        {
            if (dialogsDB)
            {
                Array.Clear(dialogs, 0, dialogs.Length);
                Array.Resize(ref dialogs, entity_Dialogue.sheets[0].list.Count);

                int ArrayCursor = 0;
                foreach (Entity_Dialog.Param param in entity_Dialogue.sheets[0].list)
                {
                    dialogs[ArrayCursor].index = param.index;
                    dialogs[ArrayCursor].speakerUIindex = param.speakerUIindex;
                    dialogs[ArrayCursor].name = param.name;
                    dialogs[ArrayCursor].dialogue = param.dialogue;
                    dialogs[ArrayCursor].characterPath = param.characterPath;
                    dialogs[ArrayCursor].nextindex = param.nextindex;
                    ArrayCursor += 1;
                }
            }
        }
        if (sceneLoad3)
        {
            if (dialogsDB)
            {
                Array.Clear(dialogs, 0, dialogs.Length);
                Array.Resize(ref dialogs, entity_Dialogue.sheets[0].list.Count);

                int ArrayCursor = 0;
                foreach (Entity_Dialog.Param param in entity_Dialogue.sheets[0].list)
                {
                    dialogs[ArrayCursor].index = param.index;
                    dialogs[ArrayCursor].speakerUIindex = param.speakerUIindex;
                    dialogs[ArrayCursor].name = param.name;
                    dialogs[ArrayCursor].dialogue = param.dialogue;
                    dialogs[ArrayCursor].characterPath = param.characterPath;
                    dialogs[ArrayCursor].nextindex = param.nextindex;
                    ArrayCursor += 1;
                }
            }
        }
    }
    //�Լ��� ���� UI�� �������ų� �Ⱥ������� ����
    private void SetActiveObjects(SpeakerUI speaker, bool visible)      //0�� true
    {
        speaker.imageDialog.gameObject.SetActive(visible);              //��ȭâ
        speaker.textName.gameObject.SetActive(visible);                 //ĳ���� �̸�
        speaker.textDialogue.gameObject.SetActive(visible);             //��ȭ
        //ȭ��ǥ ��簡 ����Ǿ��� ���� Ȱ��ȭ �Ǳ� ������
        speaker.objectArrow.SetActive(false);                           //ĳ���Ͱ� �� ��縦 ������ ������ ������ ó���� false�̴�


        Color color = speaker.imgCharacter.color;                       //ĳ���� �̹���
        if (visible)
        {
            color.a = 1;
        }
        else
        {
            color.a = 0.2f;
        }
        speaker.imgCharacter.color = color;

    }

    private void SetAllClose()
    {
        for (int i = 0; i < speakers.Length; i++)
        {
            SetActiveObjects(speakers[i], false);
        }
    }

    private void SetNextDialog(int currentIndex)
    {
        SetAllClose();
        currentDialogIndex = currentIndex;                  //���� ��縦 �����ϵ���          ó���� 0�̴�
        currentSpeakerIndex = dialogs[currentDialogIndex].speakerUIindex;       //���� ȭ�� ���� ����
        SetActiveObjects(speakers[currentSpeakerIndex], true);                  //���� ȭ���� ��ȭ ���� ������Ʈ Ȱ��ȭ
        speakers[currentSpeakerIndex].textName.text = dialogs[currentDialogIndex].name; //���� ȭ���� �̸� �ؽ�Ʈ ����
        StartCoroutine("OnTypingText");
    }

    private IEnumerator OnTypingText()
    {
        int index = 0;
        isTypingEffect = true;

        if (dialogs[currentDialogIndex].characterPath != "None")    //None�� �ƴҰ�� DB�� �־���� ����� ĳ���� �̹����� �����´�.
        {
            speakers[currentSpeakerIndex].imgCharacter.sprite =
                Resources.Load<Sprite>(dialogs[currentDialogIndex].characterPath);      //������ �� ��ο� ���ϴ� �̹����� �����ּҸ� �޾��ָ� �̹����� ����.     
        }
        while (index < dialogs[currentDialogIndex].dialogue.Length + 1)                 //index�� ��ȭ ���̺��� ���� ������
        {
            speakers[currentSpeakerIndex].textDialogue.text =
                dialogs[currentDialogIndex].dialogue.Substring(0, index);   //�ؽ�Ʈ�� �ѱ��ھ� Ÿ���� ���

            index++;
            yield return new WaitForSeconds(typingSpeed);       //0.1f�� �ӵ���
        }

        isTypingEffect = false;

        speakers[currentSpeakerIndex].objectArrow.SetActive(true);                  //��ȭ�� ��ġ�� ȭ��ǥ�� ����
    }

    public bool UpdateDialog(int currenIndex, bool InitType)           //���⼭ ����@@@@@@@@@@@@@@@@@@@@@@@@@@@      ó���� 0�� true�� �ް� ����
    {
        //��� �бⰡ 1ȸ�� ȣ��
        if (DialogInit == true && InitType == true)                     //DialogInit�� ó���� true�̴�
        {
            SetAllClose();                                              //��� ��ȭ�� ĳ���� �̹������� ���� ����
            SetNextDialog(currenIndex);
            DialogInit = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isTypingEffect == true)
            {
                isTypingEffect = false;
                StopCoroutine("OnTypingText");              //Ÿ���� ȿ���� �����ϰ� , ���� ��� ��ü�� ����Ѵ�.
                speakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue;
                //��簡 �Ϸ�Ǿ��� �� Ŀ��
                speakers[currentSpeakerIndex].objectArrow.SetActive(true);

                return false;                           //
            }



            if (dialogs[currentDialogIndex].nextindex != -100)                  //������ ��ȭ���� ������ �ȵƴٸ�
            {
                SetNextDialog(dialogs[currentDialogIndex].nextindex);           //������� ����ǰ� �ϴ� �ڵ�
            }
            else
            {
                SetAllClose();
                DialogInit = true;
                return true;
            }
        }
        return false;                   //DialogTest�� �ִ� IEnumerator Start�� ��ٸ��� ���� �ý����� ����ȴ�.
    }



    [System.Serializable]

    public struct SpeakerUI
    {
        public Image imgCharacter;          //ĳ���� �̹���
        public Image imageDialog;           //��ȭâ ImageUI
        public Text textName;               //���� ������� ĳ���� �̸� ��� TextUI
        public Text textDialogue;           //���� ��� ��� Text UI
        public GameObject objectArrow;      //��簡 �Ϸ�Ǿ��� �� ����ϴ� Ŀ�� ������Ʈ
    }

    [System.Serializable]

    public struct DialogData
    {
        public int index;                   //��� ��ȣ
        public int speakerUIindex;          //����Ŀ �迭 ��ȣ
        public string name;                 //�̸�
        public string dialogue;             //���
        public string characterPath;        //ĳ���� �̹��� ���
        public int tweenType;               //Ʈ�� ��ȣ
        public int nextindex;               //���� ���
    }
    private void OnSceneName(Scene sceneNameInfo, LoadSceneMode arg)
    {

        for (int i = 0; i < sceneNameList.Count; i++)
        {
            if (sceneNameInfo.name == sceneNameList[i])
            {
                //�� ������ �´� �Ұ��� true�� ����� �ڵ�
            }
        }
    }
}
