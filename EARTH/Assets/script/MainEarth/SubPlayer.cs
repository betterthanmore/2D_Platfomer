using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubPlayer : PlayerMainController
{
    public float boostLimit = 10;
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (gameObject.tag == "SubPlayer" && Input.GetKey(KeyCode.W) && rb.velocity.y <= boostLimit)        //ºÎ½ºÆ®
        {
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}
