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

    [Header("�÷��̾� �����Ǿ��")]
    [SerializeField] GameObject rapier;

    //====[Dash]====
    bool isDashing = false;
    bool canDash = true;
    float activeSpeed;

    //====[All]====
    private Rigidbody2D rb;
    private Rigidbody2D rapieRb;

    private Vector2 moveDir;

    private Vector2 mouseDir;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rapieRb = rapier.GetComponent<Rigidbody2D>();
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
        mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
        rb.velocity = moveDir * speed;  // �÷��̾� �̵� �Լ�(.velocity
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

    //=======================[Diraction]=======================
    void MouseDir()
    {
        Vector2 aimDir = mouseDir - rapieRb.position;
        float aimAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - 135f;
        rapieRb.rotation = aimAngle;
    }
}
