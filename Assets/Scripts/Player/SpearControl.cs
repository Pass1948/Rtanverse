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
    [SerializeField] float dashSpeed;  //30f
    [SerializeField] float dashDuration; // 0.15ff
    [SerializeField] float dashFriction; //300
    [SerializeField] float frictionDelay;

    [Header("�÷��̾� �����Ǿ��")]
    [SerializeField] Transform frontPoint;
    [SerializeField] float angle = 135f;
    [SerializeField] Collider2D repireTrigger;

    //====[Dash]====
    bool canDash = true;

    //====[All]====
    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Vector2 mouseDir;
    Vector2 aimDir;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        canDash = true;
    }

    private void FixedUpdate()
    {
        Move();
        RepierHandling();
    }

    //=======================[InputSystem]=======================
    // InputAction�� ��ϵ� �̸��� �Լ��̸��� ������ Ȯ��(�ۼ��� : On+Ŀ����̸�)

    void OnAttack()
    {
        MouseDash();
    }

    void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.y = value.Get<Vector2>().y;
    }

    //=======================[Move]=======================
    void Move()
    {
        if (!canDash) return;
        moveDir.Normalize();
        BodyDir();
        rb.velocity = moveDir * speed;  // �÷��̾� �̵� �Լ�(.velocity
    }
    // �÷��̾� ���� ��ȯ ���� ����
    void BodyDir()
    {
        // ������
        if (moveDir.x > 0)
        {
            spriteRenderer.sprite = sRender;
            spriteRenderer.flipX = true;
        }
        // ����
        else if (moveDir.x < 0)
        {
            spriteRenderer.sprite = sRender;
            spriteRenderer.flipX = false;
        }
        // ��
        if (moveDir.y > 0)
        {
            spriteRenderer.sprite = bRender;
        }
        // �Ʒ�
        else if (moveDir.y < 0)
        {
            spriteRenderer.sprite = fRender;
        }
    }

    //=======================[Diraction]=======================

    void MouseDash()
    {
        if (!canDash) return;
        StartCoroutine(DashForceCoroutine());
    }

    void RepierHandling()
    {
        mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
         aimDir = (mouseDir - rb.position).normalized;
        float aimAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - angle;
        frontPoint.transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
    }

    IEnumerator DashForceCoroutine()
    {
        canDash = false;
        // ���� �̵� ��
        Vector2 originalVelocity = rb.velocity;
        rb.velocity = Vector2.zero;

        // ���� ����
        rb.AddForce(aimDir * dashSpeed, ForceMode2D.Impulse);
        repireTrigger.enabled = true;
        yield return new WaitForSeconds(dashDuration);

        // ������ ǥ��(�̲�����)
        rb.velocity = aimDir * dashFriction * Time.fixedDeltaTime;
        yield return new WaitForSeconds(frictionDelay);

        // �⺻ �̵��� ����
        rb.velocity = originalVelocity;
        repireTrigger.enabled = false;
        canDash = true;
    }
}
