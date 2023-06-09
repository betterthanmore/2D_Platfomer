using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootHold : MonoBehaviour
{
    public bool doorOpen;
    public LayerMask isPlayers;
    public GameObject doorPos;
    public GameObject doorOpenPos;
    public GameObject doorClosePos;
    public float posTime;
    public float imgSizeInt;
    // Start is called before the first frame update
    void Start()
    {
        //나중에 현재 포시션의 +-를 써서 설정하기 쉽게 만들고 그 식은 이미지 크기에 맞춰 코드를 만들기
        posTime = doorClosePos.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        doorOpen = Physics2D.OverlapCircle(transform.position, imgSizeInt / 10, isPlayers);
        if (doorOpen)
        {
            if(doorOpenPos.transform.position.y >= posTime)     //문이 열렸을 때의 위치가 PosTime보다 클 때 (PosTime을 doorOpenPos의 위치만큼 매프레임마다 한 프레임마다 걸리는 시간 만큼 더해준다)
            {
                posTime += Time.deltaTime;
            }
            doorPos.transform.position = new Vector2(doorPos.transform.position.x, posTime);
        }
        else
        {
            if(doorClosePos.transform.position.y <= posTime)
            {
                posTime -= Time.deltaTime;
            }
            doorPos.transform.position = new Vector2(doorPos.transform.position.x, posTime);
        }
    }
}
