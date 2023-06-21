using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimumGearsSC : MonoBehaviour
{
    private GameManager GameManager => GameManager.Instance;
    // Start is called before the first frame update
    
    void Awake()
    {
        if (GameManager.minimumGears == null)
        {
            GameManager.minimumGears = gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
