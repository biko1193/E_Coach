using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    public float moveSpeed = 5.0f; //�÷��̾� �̵� �ӵ�
    public float jumpPower = 5.0f; //�÷��̾� ���� ��

    public Animator animator; // Animator �Ӽ� ���� ����
    public Rigidbody2D rigid; // Rigidbody 2D �Ӽ� ���� ����
    public SpriteRenderer rend; // SpriteRenderer �Ӽ� ���� ����

    float horizontal; //����, ������ ���Ⱚ�� �޴� ����

    bool isjumping; // ���� ���������� ��, ���� ���� ������ bool�� ����

    private void Start()
    {
        animator = GetComponent<Animator>(); //animator ������ Player�� Animator �Ӽ����� �ʱ�ȭ
        rend = GetComponent<SpriteRenderer>(); // rend ������ Player�� SpriteRenderer �Ӽ����� �ʱ�ȭ        
        rigid = GetComponent<Rigidbody2D>(); // rigid ������ Player�� Rigidbody 2D �Ӽ����� �ʱ�ȭ
    }

    private void FixedUpdate()
    {
        Move(); //�÷��̾� �̵�
        Jump(); //����

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
        if (Input.GetButton("Jump")) //���� Ű�� ������ ��
        {
            if (isjumping == false) //���� ������ ���� ��
            {
                rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse); //�������� ���� �ش�.
                isjumping = true;
            }
            else return; //���� ���� ���� �������� �ʰ� �ٷ� return.
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
                rend.flipX = false; // Player�� Sprite�� �¿������Ű�� �Լ� , true�� �� ���� 
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

        Vector3 dir = horizontal * Vector3.right; // transform.Translate() ������ �ڷ����� ���߱� ���� ������ ���ο� Vector3 ���� ����
        this.transform.Translate(dir * moveSpeed * Time.deltaTime); // Player ������Ʈ �̵� �Լ�
    }
}