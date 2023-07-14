using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MinimumGearsSC : MonoBehaviour
{
    private GameManager GameManager => GameManager.Instance;
    // Start is called before the first frame update
    
    void Awake()
    {
        if (GameManager.minimumGears == null)
        {
            GameManager.minimumGears = gameObject.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
