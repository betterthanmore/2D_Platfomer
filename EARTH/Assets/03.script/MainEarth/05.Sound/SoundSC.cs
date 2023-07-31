using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoundSC : MonoBehaviour
{
    public static bool open;
    Sequence sequence;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            sequence = DOTween.Sequence();
            sequence.Append(transform.DOScale(5, 0.4f));
            //sequence.Append(transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 1f, 10, 1));
        }
        if (!open)
        {

            transform.DOScale(0.02f, 0.2f);
        }
    }
}
