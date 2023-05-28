using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayerController : MonoBehaviour
{
    public enum PLAYERTYPE
    {
        PLAYER_01,
        PLAYER_02
    }

    public float speed = 5f;         // 플레이어 이동 속도
    public float jumpForce = 10;     // 점프 힘
    public Transform groundCheck;    // 땅 체크를 위한 오브젝트
    public LayerMask groundLayer;    // 땅을 나타내는 레이어
    public LayerMask playerLayer;    // 플레이어 레이어

    public Rigidbody2D rb;          // Rigidbody2D 컴포넌트 참조

    public bool isGrounded;         // 땅에 닿았는지 여부
    public bool isPlayerOn;         // 플레이어 위에 있는지 체크

    public string HorizontalKeyMap = "Horizontal1";
    public string JumpKeyMap = "GamePad1_A";

    public PLAYERTYPE playertype = PLAYERTYPE.PLAYER_01;

    public float moveHorizontal = 0f;

    public virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Rigidbody2D 컴포넌트 할당

        if (playertype == PLAYERTYPE.PLAYER_01)
        {
            HorizontalKeyMap = "Horizontal1";
            JumpKeyMap = "GamePad1_A";
        }
        else if (playertype == PLAYERTYPE.PLAYER_02)
        {
            HorizontalKeyMap = "Horizontal2";
            JumpKeyMap = "GamePad2_A";
        }
    }

    public virtual void Update()
    {
        // 수평 이동 입력 값
        moveHorizontal = Input.GetAxis(HorizontalKeyMap);

        // 땅 체크
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);


        // 플레이어 충돌 체크
        Collider2D[] colliders = new Collider2D[10]; // 결과를 저장할 배열 생성
        int colliderCount = Physics2D.OverlapCircleNonAlloc(groundCheck.position, 0.2f, colliders, playerLayer);
        isPlayerOn = false;

        for (int i = 0; i < colliderCount; i++)
        {
            if (colliders[i].gameObject != gameObject) // 자기 자신이 아닌 경우
            {
                isPlayerOn = true;
                break;
            }
        }

        // 점프 입력 처리
        if (Input.GetButtonDown(JumpKeyMap) && (isGrounded || isPlayerOn))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        if(transform.position.y < -30.0f)   //아래로 떨어지면 파괴
        {
            Destroy(gameObject);
        }
    }

    public virtual void FixedUpdate()
    {
        // 중력 적용
        rb.AddForce(new Vector2(0f, -9.8f * rb.mass));
    }
}



