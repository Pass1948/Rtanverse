using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpearControl : MonoBehaviour
{
    [Header("플레이어 이동속도")]
    [SerializeField] float speed;

    [Header("플레이어 이미지")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite sRender;
    [SerializeField] Sprite bRender;
    [SerializeField] Sprite fRender;

    [Header("플레이어 대시 세팅")]
    [SerializeField] float dashSpeed;  //30f
    [SerializeField] float dashDuration; // 0.15ff
    [SerializeField] float dashFriction; //300
    [SerializeField] float frictionDelay;

    [Header("플레이어 레이피어세팅")]
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
    // InputAction에 등록된 이름과 함수이름이 같은지 확인(작성법 : On+커멘드이름)

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
        rb.velocity = moveDir * speed;  // 플레이어 이동 함수(.velocity
    }
    // 플레이어 반향 전환 관련 로직
    void BodyDir()
    {
        // 오른쪽
        if (moveDir.x > 0)
        {
            spriteRenderer.sprite = sRender;
            spriteRenderer.flipX = true;
        }
        // 왼쪽
        else if (moveDir.x < 0)
        {
            spriteRenderer.sprite = sRender;
            spriteRenderer.flipX = false;
        }
        // 위
        if (moveDir.y > 0)
        {
            spriteRenderer.sprite = bRender;
        }
        // 아래
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
        // 기존 이동 끔
        Vector2 originalVelocity = rb.velocity;
        rb.velocity = Vector2.zero;

        // 힘을 가함
        rb.AddForce(aimDir * dashSpeed, ForceMode2D.Impulse);
        repireTrigger.enabled = true;
        yield return new WaitForSeconds(dashDuration);

        // 마찰력 표현(미끄러짐)
        rb.velocity = aimDir * dashFriction * Time.fixedDeltaTime;
        yield return new WaitForSeconds(frictionDelay);

        // 기본 이동력 복원
        rb.velocity = originalVelocity;
        repireTrigger.enabled = false;
        canDash = true;
    }
}
