using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpearControl : MonoBehaviour
{
    [Header("플레이어 이미지")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite bRender;
    [SerializeField] Sprite fRender;

    [Header("플레이어 대시 세팅")]
    [SerializeField] float dashSpeed;  //30f
    [SerializeField] float dashDuration; // 0.15ff
    [SerializeField] float dashFriction; //300
    [SerializeField] float frictionDelay;

    [Header("플레이어 레이피어세팅")]
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
    // InputAction에 등록된 이름과 함수이름이 같은지 확인(작성법 : On+커멘드이름)

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

        // 기존 이동 끔
        Vector2 originalVelocity = rb.velocity;
        rb.velocity = Vector2.zero;

        // 힘을 가함
        rb.AddForce(aimDir * dashSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(dashDuration);

        // 마찰력 표현(미끄러짐)
        rb.velocity = aimDir * dashFriction * Time.fixedDeltaTime;
        yield return new WaitForSeconds(frictionDelay);

        // 기본 이동력 복원
        rb.velocity = originalVelocity;
        canDash = true;
    }

    // 플레이어 반향 전환 관련 로직
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
