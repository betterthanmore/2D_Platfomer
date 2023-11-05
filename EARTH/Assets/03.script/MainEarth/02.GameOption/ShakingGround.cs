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
        {                           //sin�ȿ� ������ ���� �־��ָ� -1 ~ 1������ ������ �����ش�          ���߿� �� ���ʿ� �� ���� ������Ʈ�� ��ġ�� �ش� ��ġ�� Physics�� ��ġ�� �����Ǵ� �� �������� ȸ���ǰ� �ϱ�
            float rotationAmount = Mathf.Sin(rotation_dir * 360) * rotation_force;
            transform.rotation = originalRotation * Quaternion.Euler(0, 0, rotationAmount);
            rotation_force = Mathf.Clamp(rotation_force - Time.deltaTime * 10, 0, rotation_force_init);
            if (rotation_force == 0)
            {
                Debug.Log("�ݺ�3");
                start = false;
            }
           /* else if (num_plus)
            {
                Debug.Log("�ݺ�1");
                rotation_dir = Mathf.Clamp(rotation_dir - Time.deltaTime, -1, 1);
                if(rotation_dir == -1 && rotation_force != 0)
                {
                    num_plus = false;
                }
            }
            else if (!num_plus)
            {
                Debug.Log("�ݺ�2");
                rotation_dir = Mathf.Clamp(rotation_dir + Time.deltaTime, -1, 1);
                if(rotation_dir == 1)
                {
                    num_plus = true;
                }

            }*/
            else if (num_plus)
            {
                Debug.Log("�ݺ�1");
                rotation_dir = Mathf.Clamp(rotation_dir - Time.deltaTime * 0.1f, -1, 1);
                if (rotation_dir == -1)
                {
                    num_plus = false;
                }
            }

        }
    }
    
}
