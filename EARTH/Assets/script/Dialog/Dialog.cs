using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;                           //Arry 수정 기능을 사용하기 위해 추가
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    [SerializeField]
    private SpeakerUI[] speakers;                                       //대화에 참여하는 캐릭터들의 UI 배열
    [SerializeField]
    private DialogData[] dialogs;                                       //현재 분기의 대사 목록 배열
    [SerializeField]
    private bool DialogInit = true;                                     //자동 시작 여부
    [SerializeField]
    private bool dialogsDB = false;                                     //DB를 통해 읽는 것 설정

    public int currentDialogIndex = -1;                                 //현재 대사 순번
    public int currentSpeakerIndex = 0;                                 //현재 말을 하는 화자의 Speakers 배열 순번
    public float typingSpeed = 0.1f;                                    //텍스트 타이핑 효과의 재생 속도
    public bool isTypingEffect = false;                                 //텍스트 타이핑 효과가 재생중인지 판단.

    public List<string> sceneNameList = new List<string>();
    //여기서부터 아래 불값은 나중에 변경
    public bool sceneLoad1;
    public bool sceneLoad2;
    public bool sceneLoad3;

    public Entity_Dialog entity_Dialogue;

    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneName;
        SetAllClose();
        
        //여기 바깥 if문 나중에 변경
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
    //함수를 통해 UI가 보여지거나 안보여지게 설정
    private void SetActiveObjects(SpeakerUI speaker, bool visible)      //0과 true
    {
        speaker.imageDialog.gameObject.SetActive(visible);              //대화창
        speaker.textName.gameObject.SetActive(visible);                 //캐릭터 이름
        speaker.textDialogue.gameObject.SetActive(visible);             //대화
        //화살표 대사가 종료되었을 때만 활성화 되기 때문에
        speaker.objectArrow.SetActive(false);                           //캐릭터가 한 대사를 끝내면 나오기 때문에 처음엔 false이다


        Color color = speaker.imgCharacter.color;                       //캐릭터 이미지
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
        currentDialogIndex = currentIndex;                  //다음 대사를 진행하도록          처음에 0이다
        currentSpeakerIndex = dialogs[currentDialogIndex].speakerUIindex;       //현재 화자 순번 설정
        SetActiveObjects(speakers[currentSpeakerIndex], true);                  //현재 화자의 대화 관련 오브젝트 활성화
        speakers[currentSpeakerIndex].textName.text = dialogs[currentDialogIndex].name; //현재 화자의 이름 텍스트 설정
        StartCoroutine("OnTypingText");
    }

    private IEnumerator OnTypingText()
    {
        int index = 0;
        isTypingEffect = true;

        if (dialogs[currentDialogIndex].characterPath != "None")    //None이 아닐경우 DB에 넣어놓은 경로의 캐릭터 이미지를 가져온다.
        {
            speakers[currentSpeakerIndex].imgCharacter.sprite =
                Resources.Load<Sprite>(dialogs[currentDialogIndex].characterPath);      //엑셀의 이 경로에 원하는 이미지의 에셋주소를 달아주면 이미지가 들어간다.     
        }
        while (index < dialogs[currentDialogIndex].dialogue.Length + 1)                 //index가 대화 길이보다 작을 때까지
        {
            speakers[currentSpeakerIndex].textDialogue.text =
                dialogs[currentDialogIndex].dialogue.Substring(0, index);   //텍스트를 한글자씩 타이핑 재생

            index++;
            yield return new WaitForSeconds(typingSpeed);       //0.1f의 속도로
        }

        isTypingEffect = false;

        speakers[currentSpeakerIndex].objectArrow.SetActive(true);                  //대화를 마치면 화살표가 켜짐
    }

    public bool UpdateDialog(int currenIndex, bool InitType)           //여기서 시작@@@@@@@@@@@@@@@@@@@@@@@@@@@      처음에 0과 true를 받고 시작
    {
        //대사 분기가 1회만 호출
        if (DialogInit == true && InitType == true)                     //DialogInit는 처음에 true이다
        {
            SetAllClose();                                              //모든 대화나 캐릭터 이미지등을 끄고 시작
            SetNextDialog(currenIndex);
            DialogInit = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (isTypingEffect == true)
            {
                isTypingEffect = false;
                StopCoroutine("OnTypingText");              //타이핑 효과를 중지하고 , 현재 대사 전체를 출력한다.
                speakers[currentSpeakerIndex].textDialogue.text = dialogs[currentDialogIndex].dialogue;
                //대사가 완료되었을 때 커서
                speakers[currentSpeakerIndex].objectArrow.SetActive(true);

                return false;                           //
            }



            if (dialogs[currentDialogIndex].nextindex != -100)                  //마지막 대화까지 진행이 안됐다면
            {
                SetNextDialog(dialogs[currentDialogIndex].nextindex);           //다음대사 진행되게 하는 코드
            }
            else
            {
                SetAllClose();
                DialogInit = true;
                return true;
            }
        }
        return false;                   //DialogTest에 있는 IEnumerator Start의 기다림이 끝나 시스템이 종료된다.
    }



    [System.Serializable]

    public struct SpeakerUI
    {
        public Image imgCharacter;          //캐릭터 이미지
        public Image imageDialog;           //대화창 ImageUI
        public Text textName;               //현재 대사중인 캐릭터 이름 출력 TextUI
        public Text textDialogue;           //현재 대사 출력 Text UI
        public GameObject objectArrow;      //대사가 완료되었을 때 출력하는 커서 오브젝트
    }

    [System.Serializable]

    public struct DialogData
    {
        public int index;                   //대사 번호
        public int speakerUIindex;          //스피커 배열 번호
        public string name;                 //이름
        public string dialogue;             //대사
        public string characterPath;        //캐릭터 이미지 경로
        public int tweenType;               //트윈 번호
        public int nextindex;               //다음 대사
    }
    private void OnSceneName(Scene sceneNameInfo, LoadSceneMode arg)
    {

        for (int i = 0; i < sceneNameList.Count; i++)
        {
            if (sceneNameInfo.name == sceneNameList[i])
            {
                //씬 순서에 맞는 불값을 true로 만드는 코드
            }
        }
    }
}
