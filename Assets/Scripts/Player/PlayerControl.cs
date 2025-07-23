using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    [Header("플레이어 이동속도")]
    [SerializeField] float speed;

    [Header("플레이어 점프")]
    [SerializeField] float jumpPower;
    [SerializeField] float gravity;
    [SerializeField] int maxJumpCount;

    [Header("플레이어 이미지")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite sRender;
    [SerializeField] Sprite bRender;
    [SerializeField] Sprite fRender;
    [SerializeField] float shadowMinSize;
    [SerializeField] float sizeSpeed;

    [Header("플레이어 위치 오브젝트")]
    [SerializeField] Transform body;
    [SerializeField] Transform shadow;

    [Header("Raycast길이")]
    [SerializeField] float rayLangth;


    private int jumpCount = 0; // 점프 카운터

    private float curJumpP; // 점프 파워
    private float groundOffset = 0.0f;  // 바닥감지 기준수치


    private bool isMinSize = false;
    private bool isGround = true; // 점프 가능 상태

    private Rigidbody2D rd;

    private Vector2 moveDir;

    private Vector3 dirVec;


    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
        DrawRay();
    }

    private void Update()
    {
        JumpHandling();
    }

    //=======================[InputSystem]=======================
    // InputAction에 등록된 이름과 함수이름이 같은지 확인(작성법 : On+커멘드이름)


    void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.y = value.Get<Vector2>().y;
    }

    void OnJump()
    {
        Jump(jumpPower);
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
        if(moveDir.x > 0)
        {
            dirVec = Vector3.right;
            spriteRenderer.sprite = sRender;
            spriteRenderer.flipX = true;
        }
        else if(moveDir.x < 0)
        {
            dirVec = Vector3.left;
            spriteRenderer.sprite = sRender;
            spriteRenderer.flipX = false;
        }

        if(moveDir.y > 0)
        {
            dirVec = Vector3.up;
            spriteRenderer.sprite = bRender;
        }
        else if(moveDir.y < 0)
        {
            dirVec = Vector3.down;
            spriteRenderer.sprite = fRender;
        }
    }

    //=======================[Jump]=======================
    void Jump(float jumpPower)
    {
        if (jumpCount >= maxJumpCount) return;
             curJumpP = jumpPower;
             isGround = false;
             jumpCount++;
    }

    void JumpHandling()
    {
        if (isGround==true)
            return;

        body.transform.position += new Vector3 (0.0f, curJumpP * Time.deltaTime, 0.0f);
        curJumpP -= gravity * Time.deltaTime;

        if (shadow.transform.localScale.x > shadowMinSize&& isMinSize==false || shadow.transform.localScale.y > shadowMinSize&& isMinSize== false)
                shadow.transform.localScale -= new Vector3(shadowMinSize * Time.deltaTime, shadowMinSize * Time.deltaTime, 0.0f);
        else
        {
            isMinSize = true;
            shadow.transform.localScale += new Vector3(sizeSpeed * Time.deltaTime, sizeSpeed * Time.deltaTime, 0.0f);
        }

        if (body.transform.position.y <= shadow.position.y+ groundOffset&&curJumpP <= 0.0f)
        {
            shadow.transform.localScale = new Vector3(1f, 1f, 0.0f);
            isMinSize = false;
            curJumpP = 0.0f;
            isGround = true;
            jumpCount = 0;
            body.transform.position = new Vector2(body.transform.position.x, shadow.position.y + groundOffset);
        }
    }

    //=======================[Interaction RayCast]=======================
    void DrawRay()
    {
        Debug.DrawRay(rd.position, dirVec * rayLangth, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rd.position, dirVec, rayLangth);
    }








}
