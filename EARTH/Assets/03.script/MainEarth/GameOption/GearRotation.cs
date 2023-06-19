using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearRotation : MonoBehaviour
{
    private float timer;
    private int[] rotateDir = new int[2] { 1, -1 };
    private int dir;
    public int randomNum;
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        randomNum = Random.Range(0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime * rotateDir[randomNum]);
    }
}
