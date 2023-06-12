using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainKeyBoard : PlayerMainController
{
    public bool isPlayerOn;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        isPlayerOn = Physics2D.OverlapCircle(groundCheck.position, 0.2f, playerLayer);
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(Input.GetAxis("HorizontalMain") * moveSpeed, rb.velocity.y);
        }
        if(Input.GetKeyDown(KeyCode.UpArrow) && (isPlayerOn || isGround))
        {
            rb.AddForce(new Vector2(rb.velocity.x, jumpForce), ForceMode2D.Impulse);
        }
        if (gameObject.tag == "MainPlayer" && Input.GetKeyUp(KeyCode.LeftArrow) || gameObject.tag == "MainPlayer" && Input.GetKeyUp(KeyCode.RightArrow) //¸ÞÀÎÇÃ·¹ÀÌ¾î°¡ ÁÂ,¿ì·Î ¿òÁ÷ÀÓÀ» ¸ØÃâ ¶§ ¹Ù·Î ¸ØÃß°Ô ÇÏ±â
        || gameObject.tag == "SubPlayer" && Input.GetKeyUp(KeyCode.A) || gameObject.tag == "SubPlayer" && Input.GetKeyUp(KeyCode.D))                    //¼­ºêÇÃ·¹ÀÌ¾î°¡ ÁÂ,¿ì·Î ¿òÁ÷ÀÓÀ» ¸ØÃâ ¶§ ¹Ù·Î ¸ØÃß°Ô ÇÏ±â
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            an.SetBool("Run", false);        //°È´Â ¸ð¼Ç ÁßÁö

        }
        if (!isGround && isPlayerOn || isGround && !isPlayerOn)      //¶¥À» ¹â°í ÀÖÁö ¾Ê´Ù¸é °È´Â ¸ð¼Ç ÁßÁö
        {
            an.SetBool("Jump", false);
        }
        else//¶¥À» ¹â°í ÀÖ´Ù¸é ¸ð¼Ç Á¡ÇÁ ÁßÁö
        {
            an.SetBool("Run", false);
            an.SetBool("Jump", true);
        }
    }
    
}
