using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingGround : MonoBehaviour
{
    public LayerMask all_player;
    public BoxCollider2D bc;
    public Rigidbody2D rb;
    public Collider2D[] sence;
    public int current_sence;
    public float rotation_dir;
    float rotation_dir_init;
    public bool start;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    public float rotation_force;
    float rotation_force_init;
    private bool num_plus;
    // Start is called before the first frame update
    private void Start()
    {
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        rotation_force_init = rotation_force;
        rotation_dir_init = rotation_dir;
    }

    // Update is called once per frame
    void Update()
    {
        sence = Physics2D.OverlapBoxAll(transform.position, bc.size, 0, all_player);
        if (current_sence != sence.Length)
        {
            rotation_dir = rotation_dir_init;
            rotation_force = rotation_force_init;
            start = true;
            num_plus = true;
        }
        current_sence = sence.Length;

        if (start)
        {                           //sin안에 각도의 값을 넣어주면 -1 ~ 1까지의 값으로 돌려준다          나중에 땅 끝쪽에 빈 게임 오브젝트를 배치해 해당 위치에 Physics를 설치해 감지되는 쪽 기준으로 회전되게 하기
            float rotationAmount = Mathf.Sin(rotation_dir * 360) * rotation_force;
            transform.rotation = originalRotation * Quaternion.Euler(0, 0, rotationAmount);
            rotation_force = Mathf.Clamp(rotation_force - Time.deltaTime * 10, 0, rotation_force_init);
            if (rotation_force == 0)
            {
                Debug.Log("반복3");
                start = false;
            }
           /* else if (num_plus)
            {
                Debug.Log("반복1");
                rotation_dir = Mathf.Clamp(rotation_dir - Time.deltaTime, -1, 1);
                if(rotation_dir == -1 && rotation_force != 0)
                {
                    num_plus = false;
                }
            }
            else if (!num_plus)
            {
                Debug.Log("반복2");
                rotation_dir = Mathf.Clamp(rotation_dir + Time.deltaTime, -1, 1);
                if(rotation_dir == 1)
                {
                    num_plus = true;
                }

            }*/
            else if (num_plus)
            {
                Debug.Log("반복1");
                rotation_dir = Mathf.Clamp(rotation_dir - Time.deltaTime * 0.1f, -1, 1);
                if (rotation_dir == -1)
                {
                    num_plus = false;
                }
            }

        }
    }
    
}
