using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakingGround : MonoBehaviour
{
    public LayerMask all_player;
    
    public float rotation_force;
    public GameObject left;
    public GameObject right;
    public GameObject center;
    [Header("감지 사이즈")]
    public BoxCollider2D bc;
    public Rigidbody2D rb;
    public Collider2D[] shaking_sencer;
    public float rotation_dir;
    public bool left_main_player = false;
    public bool left_sub_player = false;
    public bool right_main_player = false;
    public bool right_sub_player = false;

    [SerializeField]
    Collider2D[] right_sence;
    [SerializeField]
    Collider2D[] left_sence;
    bool start;
    Quaternion originalRotation;
    float rotation_force_init;
    MainPlayer mainPlayer;
    SubPlayer subPlayer;
    // Start is called before the first frame update
    private void Start()
    {
        originalRotation = transform.rotation;
        rotation_force_init = rotation_force;
        mainPlayer = GameObject.FindGameObjectWithTag("MainPlayer").GetComponent<MainPlayer>();
        subPlayer = GameObject.FindGameObjectWithTag("SubPlayer").GetComponent<SubPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        //shaking_sencer = Physics2D.OverlapBoxAll(center.transform.position, bc.size + new Vector2(0, bc.size.y), 0, all_player);        //범위 벗어나는 것만 체크 나갈 때 false로 바꿈
        right_sence = Physics2D.OverlapBoxAll(right.transform.position, bc.size + new Vector2(-bc.size.x / 2, 0.1f), 0, all_player);       //들어오는 것만 체크 들어올 때 true로 바꿈 false일 때 true로 바뀌면 흔들리는 로직 실행
        left_sence = Physics2D.OverlapBoxAll(left.transform.position, bc.size + new Vector2(-bc.size.x / 2, 0.1f), 0, all_player);
        if (right_sence.Length != 0)
        {
            Right_Censer();
        }
        if (left_sence.Length != 0)
        {
            Left_Censer();
        }
        if(Input.GetKey("joystick 1 button 1") || Input.GetKey("joystick 2 button 1"))
        {

        }
        /*if (left_sence.Length != 0 || right_sence.Length != 0)
        {
            Rotation_Controller();
        }
        else
        {
            right_sub_player = false;
            right_main_player = false;
        }*/
        
        if (start)
        {                           //sin안에 각도의 값을 넣어주면 -1 ~ 1까지의 값으로 돌려준다          나중에 땅 끝쪽에 빈 게임 오브젝트를 배치해 해당 위치에 Physics를 설치해 감지되는 쪽 기준으로 회전되게 하기
            float rotationAmount = Mathf.Sin(rotation_dir * 180) * rotation_force;
            transform.rotation = originalRotation * Quaternion.Euler(0, 0, rotationAmount);
            rotation_force = Mathf.Clamp(rotation_force - Time.deltaTime * 10, 0, rotation_force_init);
            if (rotation_force == 0)
            {
                start = false;
            }
            else
            {
                rotation_dir = Mathf.Clamp(rotation_dir - Time.deltaTime * rotation_dir * 0.1f, -1, 1);
            }
        }
    }
   
    public void Left_Censer()
    {
        if(left_sence.Length == 1)
        {
            if (left_sence[0].gameObject.layer == 8)
            {
                
                if (mainPlayer.jump)
                {
                    mainPlayer.jump = false;
                    rotation_dir = 1;
                    Rotation_start();
                }
            }
            else if (left_sence[0].gameObject.layer == 9)
            {
                
                if (subPlayer.jump)
                {
                    subPlayer.jump = false;
                    rotation_dir = 1;
                    Rotation_start();
                }
            }
            
        }
        else if(left_sence.Length == 2)
        {
            foreach (var item in left_sence)
            {
                if(item.gameObject.layer == 8)
                {
                    if (mainPlayer.jump)
                    {
                        mainPlayer.jump = false;
                        rotation_dir = 1;
                        Rotation_start();
                    }
                }
                if(item.gameObject.layer == 9) 
                {
                    if (subPlayer.jump)
                    {
                        subPlayer.jump = false;
                        rotation_dir = 1;
                        Rotation_start();
                    }
                }
            }
        }
        else
        {
            left_main_player = false;
            left_sub_player = false;
        }
        
    }
    public void Right_Censer()
    {
        if (right_sence.Length == 1)
        {
            if (right_sence[0].gameObject.layer == 8)
            {

                if (mainPlayer.jump)
                {
                    mainPlayer.jump = false;
                    rotation_dir = -1;
                    Rotation_start();
                }
            }
            else if (right_sence[0].gameObject.layer == 9)
            {

                if (subPlayer.jump)
                {
                    subPlayer.jump = false;
                    rotation_dir = -1;
                    Rotation_start();
                }
            }

        }
        else if (right_sence.Length == 2)
        {
            foreach (var item in right_sence)
            {
                if (item.gameObject.layer == 8)
                {
                    if (mainPlayer.jump)
                    {
                        mainPlayer.jump = false;
                        rotation_dir = -1;
                        Rotation_start();
                    }
                }
                if (item.gameObject.layer == 9)
                {
                    if (subPlayer.jump)
                    {
                        subPlayer.jump = false;
                        rotation_dir = -1;
                        Rotation_start();
                    }
                }
            }
        }
        else
        {
            right_main_player = false;
            right_sub_player = false;
        }
    }
    /*public void Rotation_Controller()
    {
        if (left_sence.Length == 2)
        {
            foreach (var item in left_sence)
            {
                if (item.gameObject.layer == 8)
                {
                    if (!right_main_player && !left_main_player)
                    {
                        rotation_dir = 1;
                        Rotation_start();
                    }

                    right_main_player = false;
                    left_main_player = true;
                }
                else if (item.gameObject.layer == 9)
                {
                    if (!right_sub_player && !left_sub_player)
                    {
                        rotation_dir = 1;
                        Rotation_start();
                    }
                    left_sub_player = true;
                    right_sub_player = false;
                }
            }
        }
        else if (left_sence.Length == 1)
        {
            //2에서 1이 됐을 때 흔들리는 로직
            foreach (var item in left_sence)
            {
                if (item.gameObject.layer != 8)
                {

                    if (!left_sub_player)
                    {
                        left_sub_player = true;
                        if (!right_sub_player)
                        {
                            rotation_dir = 1;
                            Rotation_start();
                        }

                    }
                    else if (left_main_player)
                    {
                        left_main_player = false;
                        if (!right_main_player)
                        {
                            rotation_dir = 1;
                            Rotation_start();
                        }
                    }
                }
                else
                {

                    if (!left_main_player)
                    {
                        left_main_player = true;
                        if (!right_main_player)
                        {
                            rotation_dir = 1;
                            Rotation_start();
                        }

                    }
                    else if (left_sub_player)
                    {
                        left_sub_player = false;
                        if (!right_sub_player)
                        {
                            rotation_dir = 1;
                            Rotation_start();
                        }
                    }
                }
            }
        }
        else
        {
            if (left_main_player || left_sub_player)
            {
                left_main_player = false;
                left_sub_player = false;
                rotation_dir = 1;
                Rotation_start();
            }

        }

        if (right_sence.Length == 2)
        {
            foreach (var item in right_sence)
            {
                if (item.gameObject.layer == 8)
                {
                    if (!right_main_player && !left_main_player)
                    {
                        rotation_dir = -1;
                        Rotation_start();
                    }
                    left_main_player = false;
                    right_main_player = true;
                }
                else if (item.gameObject.layer == 9)
                {
                    if (!right_sub_player && !left_sub_player)
                    {
                        rotation_dir = -1;
                        Rotation_start();
                    }
                    right_sub_player = true;
                    left_sub_player = false;
                }
            }
        }
        else if (right_sence.Length == 1)
        {
            foreach (var item in right_sence)
            {
                if (item.gameObject.layer != 8)
                {
                    if (!right_sub_player)
                    {
                        right_sub_player = true;
                        if (!left_sub_player)
                        {
                            rotation_dir = -1;
                            Rotation_start();
                        }

                    }
                    else if (right_main_player)
                    {
                        right_main_player = false;
                        if (!left_main_player)
                        {
                            rotation_dir = -1;
                            Rotation_start();
                        }
                    }
                }
                else
                {
                    if (!right_main_player)
                    {
                        right_main_player = true;
                        if (!left_main_player)
                        {
                            rotation_dir = -1;
                            Rotation_start();
                        }

                    }
                    else if (right_sub_player)
                    {
                        right_sub_player = false;
                        if (!left_sub_player)
                        {
                            rotation_dir = -1;
                            Rotation_start();
                        }
                    }
                }
            }
        }
        else
        {
            if (right_main_player || right_sub_player)
            {
                right_main_player = false;
                right_sub_player = false;
                rotation_dir = -1;
                Rotation_start();
            }

        }
    }*/
    public void Rotation_start()
    {
        rotation_force = rotation_force_init;
        start = true;
    }
    
}
