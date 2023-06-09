using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayer : PlayerMainController
{
   
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (gameObject.tag == "MainPlayer" && Input.GetButtonDown(JumpKeyMap) && (isGround))
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        if (!isGround)      //¶¥À» ¹â°í ÀÖÁö ¾Ê´Ù¸é °È´Â ¸ð¼Ç ÁßÁö
        {
            an.SetBool("Run", false);
            an.SetBool("Jump", true);
        }
        else//¶¥À» ¹â°í ÀÖ´Ù¸é ¸ð¼Ç Á¡ÇÁ ÁßÁö
        {
            an.SetBool("Jump", false);
        }
    }
}
