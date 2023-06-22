using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeRecovery : MonoBehaviour
{
    private Scrollbar scrollbar;
    public LayerMask subPlayer;
    public bool isSubPlayer = false;
    private float recTimer;
    // Start is called before the first frame update
    void Start()
    {
        scrollbar = GameObject.Find("Scrollbar").GetComponent<Scrollbar>();
    }

    // Update is called once per frame
    void Update()
    {
        isSubPlayer = Physics2D.OverlapBox(transform.position, new Vector2(1, 1), 0, subPlayer);
        if (isSubPlayer)
        {
            scrollbar.size += Time.deltaTime / 20;
            
        }
    }
}
