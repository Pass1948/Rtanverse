using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpearControl : MonoBehaviour
{
    [Header("�÷��̾� �̹���")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite bRender;
    [SerializeField] Sprite fRender;

    [Header("�÷��̾� ��� ����")]
    [SerializeField] float dashSpeed;  //30f
    [SerializeField] float dashDuration; // 0.15ff
    [SerializeField] float dashFriction; //300
    [SerializeField] float frictionDelay;

    [Header("�÷��̾� �����Ǿ��")]
    [SerializeField] Transform rapier;
    [SerializeField] float angle = 135f;

    //====[Dash]====
    bool canDash = true;

    //====[All]====
    private Rigidbody2D rb;

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
        RepierHandling();
    }

    //=======================[InputSystem]=======================
    // InputAction�� ��ϵ� �̸��� �Լ��̸��� ������ Ȯ��(�ۼ��� : On+Ŀ����̸�)

    void OnAttack()
    {
        MouseDash();
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
        rapier.transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
        BodyDir();
    }

    IEnumerator DashForceCoroutine()
    {
        canDash = false;

        // ���� �̵� ��
        Vector2 originalVelocity = rb.velocity;
        rb.velocity = Vector2.zero;

        // ���� ����
        rb.AddForce(aimDir * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashDuration);

        // ������ ǥ��(�̲�����)
        rb.velocity = aimDir * dashFriction * Time.fixedDeltaTime;
        yield return new WaitForSeconds(frictionDelay);

        // �⺻ �̵��� ����
        rb.velocity = originalVelocity;
        canDash = true;
    }

    // �÷��̾� ���� ��ȯ ���� ����
    void BodyDir()
    {
        if (mouseDir.y > 0)
        {
            spriteRenderer.sprite = bRender;
        }
        else if (mouseDir.y < 0)
        {
            spriteRenderer.sprite = fRender;
        }
    }
}
