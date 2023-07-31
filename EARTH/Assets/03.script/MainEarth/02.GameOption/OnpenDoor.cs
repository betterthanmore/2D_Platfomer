using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnpenDoor : MonoBehaviour
{
    public GameObject door;
    public bool doorOpen;
    public LayerMask isPlayers;
    public float imgSizeInt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doorOpen = Physics2D.OverlapCircle(transform.position, imgSizeInt / 10, isPlayers);
        if (doorOpen)
        {
            door.transform.rotation = Quaternion.Euler(new Vector2(0, 0));
            door.transform.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = true;
        }
        else
        {
            door.transform.rotation = Quaternion.Euler(new Vector2(0, 80));
            door.transform.GetChild(0).GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
