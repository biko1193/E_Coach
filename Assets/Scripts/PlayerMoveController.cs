using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float moveSpeed = 5.0f; //플레이어 이동 속도
    public float jumpPower = 5.0f; //플레이어 점프 힘

    public Animator animator; // Animator 속성 변수 생성
    public Rigidbody2D rigid; // Rigidbody 2D 속성 변수 생성
    public SpriteRenderer rend; // SpriteRenderer 속성 변수 생성

    float horizontal; //왼쪽, 오른쪽 방향값을 받는 변수

    bool isjumping; // 현재 점프중인지 참, 거짓 값을 가지는 bool형 변수

    private void Start()
    {
        animator = GetComponent<Animator>(); //animator 변수를 Player의 Animator 속성으로 초기화
        rend = GetComponent<SpriteRenderer>(); // rend 변수를 Player의 SpriteRenderer 속성으로 초기화        
        rigid = GetComponent<Rigidbody2D>(); // rigid 변수를 Player의 Rigidbody 2D 속성으로 초기화
    }

    private void FixedUpdate()
    {
        Move(); //플레이어 이동
        Jump(); //점프

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            isjumping = false;
        }
    }

    void Jump()
    {
        if (Input.GetButton("Jump")) //점프 키가 눌렸을 때
        {
            if (isjumping == false) //점프 중이지 않을 때
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //위쪽으로 힘을 준다.
                isjumping = true;
            }
            else return; //점프 중일 때는 실행하지 않고 바로 return.
        }
    }

    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");

        if (horizontal != 0)
        {
            animator.SetBool("walk", true);

            if (horizontal > 0)
            {
                rend.flipX = false; // Player의 Sprite를 좌우반전시키는 함수 , true일 때 반전 
            }
            else
            {
                rend.flipX = true;
            }

        }
        else
        {
            animator.SetBool("walk", false);
        }

        Vector3 dir = horizontal * Vector3.right; // transform.Translate() 변수의 자료형을 맞추기 위해 생성한 새로운 Vector3 변수 생성
        this.transform.Translate(dir * moveSpeed * Time.deltaTime); // Player 오브젝트 이동 함수
    }
}