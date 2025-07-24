using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpearControl : MonoBehaviour
{

    [Header("�÷��̾� �̵��ӵ�")]
    [SerializeField] float speed;

    [Header("�÷��̾� �̹���")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite sRender;
    [SerializeField] Sprite bRender;
    [SerializeField] Sprite fRender;

    [Header("�÷��̾� ��� ����")]
    [SerializeField] float dashSpeed;  //10f
    [SerializeField] float dashDuration; // 1f
    [SerializeField] float dashCoolDown; //1f

    //====[Dash]====
    bool isDashing = false;
    bool canDash = true;
    float activeSpeed;

    //====[All]====
    private Rigidbody2D rd;

    private Vector2 moveDir;

    private Vector3 MouseDir;

    GameObject scanOdj;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        activeSpeed = speed;
        canDash = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
  
    }

    //=======================[InputSystem]=======================
    // InputAction�� ��ϵ� �̸��� �Լ��̸��� ������ Ȯ��(�ۼ��� : On+Ŀ����̸�)


    void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.y = value.Get<Vector2>().y;
    }

    void OnSprint()
    {
        Dash();
    }


    void OnAttack()
    {

    }


    //=======================[Move]=======================
    void Move()
    {
        moveDir.Normalize();
        BodyDir();
        rd.velocity = moveDir * speed;  // �÷��̾� �̵� �Լ�(.velocity
    }

    // �÷��̾� ���� ��ȯ ���� ����
    void BodyDir()
    {
        if (moveDir.x > 0)
        {
            spriteRenderer.sprite = sRender;
            spriteRenderer.flipX = true;
        }
        else if (moveDir.x < 0)
        {
            spriteRenderer.sprite = sRender;
            spriteRenderer.flipX = false;
        }

        if (moveDir.y > 0)
        {
            spriteRenderer.sprite = bRender;
        }
        else if (moveDir.y < 0)
        {
            spriteRenderer.sprite = fRender;
        }
    }

    //=======================[Dash]=======================

    void Dash()
    {
        if (!canDash) { return; }
        isDashing = true;
        StartCoroutine(DashCorutine());
    }

    IEnumerator DashCorutine()
    {
        canDash = false;
        speed = dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        speed = activeSpeed;
        isDashing = false;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }






}
