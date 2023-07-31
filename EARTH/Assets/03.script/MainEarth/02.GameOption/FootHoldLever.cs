using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootHoldLever : MonoBehaviour
{
    public bool leverRotate;
    public LayerMask isPlayers;
    public float imgSizeInt;
    public GameObject moveIronFrame;
    public GameObject UpPos;
    public GameObject DownPos;
    public float posTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leverRotate = Physics2D.OverlapCircle(transform.position, imgSizeInt / 10, isPlayers);
        if (!leverRotate && UpPos.transform.position.y >= posTimer)
        {
            posTimer += Time.deltaTime;
            moveIronFrame.transform.position = new Vector2(moveIronFrame.transform.position.x, posTimer);
        }
        else if(leverRotate && DownPos.transform.position.y <= posTimer)
        {
            posTimer -= Time.deltaTime;
            moveIronFrame.transform.position = new Vector2(moveIronFrame.transform.position.x, posTimer);
        }
    }
}
