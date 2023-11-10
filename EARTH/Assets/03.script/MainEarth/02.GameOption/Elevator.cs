using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public GameObject left_rope;
    public GameObject left_tile;
    public GameObject right_rope;
    public GameObject right_tile;
    public BoxCollider2D bc_right;
    public BoxCollider2D bc_left;
    public LayerMask player;

    [Header("최대")]
    public float left_max;
    public float right_max;
    [Header("최소")]
    public float left_min;
    public float right_min;
    [Header("한 곳에 1명일 때 내려가는 속도")]
    public float speed1;
    [Header("한 곳에 2명일 때 내려가는 속도")]
    public float speed2;

    public Collider2D[] left_sencer;
    public Collider2D[] right_sencer;
    public float test;
    // Update is called once per frame
    void Update()
    {
        left_sencer = Physics2D.OverlapBoxAll(left_tile.transform.position, bc_left.size + new Vector2(0, test), 0, player);
        right_sencer = Physics2D.OverlapBoxAll(right_tile.transform.position, bc_right.size + new Vector2(0, test), 0, player);
        
        if (left_sencer.Length != 0 || right_sencer.Length != 0)
        {
            /*if(left_sencer.Length > 0)        해야될게 그라운드 센서에 인식이 되면 자식 오브젝트로 들어가게 해서 한곳에 2명이 서 있을 때 더 빨리 내려가는 걸 잘작동되게 해보자
            {
                foreach (var item in left_sencer)
                {   

                }
            }
            if (right_sencer.Length > 0)
            {
                foreach (var item in left_sencer)
                {

                }
            }*/
            
            if (left_sencer.Length != right_sencer.Length)
            {
                if (left_sencer.Length < right_sencer.Length)
                {
                    if (right_sencer.Length - left_sencer.Length == 1)
                    {
                        Right_Down1();
                    }
                    else
                    {
                        Right_Down2();
                    }

                }
                else
                {
                    if (left_sencer.Length - right_sencer.Length == 1)
                    {
                        Left_Down1();
                    }
                    else
                    {
                        Left_Down2();
                    }
                }
            }
        }
    }
    #region 상세 로직
    public Vector3 Elevator_up(Vector3 target, float min_size, float max_size, float speed)
    {
        target.y = Mathf.Clamp(target.y -= Time.deltaTime * speed, min_size, max_size);
        return target;
    }
    public Vector3 Elevatorh_down(Vector3 target, float min_size, float max_size, float speed)
    {
        target.y = Mathf.Clamp(target.y += Time.deltaTime * speed, min_size, max_size);
        return target;
    }
    #endregion
    #region 가독성을 위해
    public void Left_Down1()
    {
        left_rope.transform.localScale = Elevatorh_down(left_rope.transform.localScale, left_min, left_max, speed1);
        left_tile.transform.localPosition = Elevatorh_down(left_tile.transform.localPosition, -left_max, -left_min, -speed1);

        right_rope.transform.localScale = Elevator_up(right_rope.transform.localScale, right_min, right_max, speed1);
        right_tile.transform.localPosition = Elevator_up(right_tile.transform.localPosition, -right_max, -right_min, -speed1);

    }
    public void Right_Down1()
    {
        left_rope.transform.localScale = Elevator_up(left_rope.transform.localScale, left_min, left_max, speed1);
        left_tile.transform.localPosition = Elevator_up(left_tile.transform.localPosition, -left_max, -left_min, -speed1);

        right_rope.transform.localScale = Elevatorh_down(right_rope.transform.localScale, right_min, right_max, speed1);
        right_tile.transform.localPosition = Elevatorh_down(right_tile.transform.localPosition, -right_max, -right_min, -speed1);
    }
    public void Left_Down2()
    {
        left_rope.transform.localScale = Elevatorh_down(left_rope.transform.localScale, left_min, left_max, speed2);
        left_tile.transform.localPosition = Elevatorh_down(left_tile.transform.localPosition, -left_max, -left_min, -speed2);

        right_rope.transform.localScale = Elevator_up(right_rope.transform.localScale, right_min, right_max, speed2);
        right_tile.transform.localPosition = Elevator_up(right_tile.transform.localPosition, -right_max, -right_min, -speed2);
    }
    public void Right_Down2()
    {
        left_rope.transform.localScale = Elevator_up(left_rope.transform.localScale, left_min, right_max, speed2);
        left_tile.transform.localPosition = Elevator_up(left_tile.transform.localPosition, -right_max, -left_min, -speed2);

        right_rope.transform.localScale = Elevatorh_down(right_rope.transform.localScale, right_min, right_max, speed2);
        right_tile.transform.localPosition = Elevatorh_down(right_tile.transform.localPosition, -right_max, -right_min, -speed2);
    }
    #endregion
}
