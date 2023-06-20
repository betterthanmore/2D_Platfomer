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
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPush)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MainPlayer" || collision.gameObject.tag == "SubPlayer")
        {
            collision.rigidbody.velocity = new Vector2(collision.rigidbody.velocity.x / 2, collision.rigidbody.velocity.y);
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
}
