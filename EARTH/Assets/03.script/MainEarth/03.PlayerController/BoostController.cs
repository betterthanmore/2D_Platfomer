using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostController : MonoBehaviour
{
    public Animator an;
    public SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        an = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetAxis("Horizontal2") != 0 || Input.GetAxis("HorizontalSub") != 0)
        {
            an.SetBool("Run", true);
            if (Input.GetAxis("Horizontal2") > 0 || Input.GetAxis("HorizontalSub") > 0)
                sr.flipX = false;
            else if (Input.GetAxis("Horizontal2") < 0 || Input.GetAxis("HorizontalSub") < 0)
                sr.flipX = true;
        }
        else
        {
            an.SetBool("Run", false);
        }

        if(Input.GetAxis("GamePad2_A") > 0 || Input.GetKey(KeyCode.UpArrow))
        {
            an.SetBool("Jump", true);
        }
        else
        {
            an.SetBool("Jump", false);
        }

        if(Input.GetKeyDown(KeyCode.L) || Input.GetButtonDown("GamePad2_X"))
        {
            an.SetBool("Hold", true);
        }
    }
}
