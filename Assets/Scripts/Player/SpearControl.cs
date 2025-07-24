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
    // InputAction에 등록된 이름과 함수이름이 같은지 확인(작성법 : On+커멘드이름)


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
        rd.velocity = moveDir * speed;  // 플레이어 이동 함수(.velocity
    }

    // 플레이어 반향 전환 관련 로직
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
