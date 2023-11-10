using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public GameObject moveObject;
    public float speed;
    public GameObject pulley;
    public float dir = 1;

    public float min;
    public float max;



    // Update is called once per frame
    void Update()
    {
        moveObject.transform.localPosition = new Vector3(Mathf.Clamp(moveObject.transform.localPosition.x + Time.deltaTime * speed, min, max), moveObject.transform.localPosition.y, moveObject.transform.localPosition.z);
        if (moveObject.transform.localPosition.x  == min || moveObject.transform.localPosition.x == max)
        {
            dir = dir * - 1;
            speed = speed * dir;
            Debug.Log("π›¿¿");
        }
    }
}
