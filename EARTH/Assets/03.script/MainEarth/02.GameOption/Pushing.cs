using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushing : MonoBehaviour
{
    private bool isPush;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPush && transform.parent == null)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MainPlayer" || collision.gameObject.tag == "SubPlayer")
        {
            isPush = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MainPlayer" || collision.gameObject.tag == "SubPlayer")
        {
            isPush = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainPlayer" || collision.gameObject.tag == "SubPlayer")
        {
            /*if (collision.GetComponent<PlayerMainController>().state != PlayerMainController.State.HOLD)
            {
                collision.GetComponent<PlayerMainController>().moveSpeed = 0.5f;
            } */
            if (collision.TryGetComponent<PlayerMainController>(out PlayerMainController pm))
            {
                if (pm.state != PlayerMainController.State.HOLD)
                {
                    pm.moveSpeed = 0.5f;
                }
            } 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "MainPlayer" || collision.gameObject.tag == "SubPlayer")
        {
            if (collision.GetComponent<PlayerMainController>().state != PlayerMainController.State.HOLD)
            {
                    collision.GetComponent<PlayerMainController>().moveSpeed = 2;
            } 
        }
    }

}
