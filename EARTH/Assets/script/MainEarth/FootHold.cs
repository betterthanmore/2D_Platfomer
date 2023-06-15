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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doorOpen = Physics2D.OverlapCircle(transform.position, 0.7f, isPlayers);
        if (doorOpen)
        {
            if(doorOpenPos.transform.position.y >= posTime)
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
