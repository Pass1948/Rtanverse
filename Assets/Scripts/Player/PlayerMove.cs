using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
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

    [Header("위치")]
    [SerializeField] Transform body;
    [SerializeField] Transform shdow;

    private int jumpCount = 0;

    private float curJumpP;
    private float groundOffset = 0.0f;

    private bool isGround = true; // 점프 가능 상태

    private Rigidbody2D rd;

    private Vector2 moveDir;


    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        JumpHandling();
    }

    void Move()
    {
        moveDir.Normalize();
        BodyDir();
        rd.velocity = moveDir * speed;
    }

    void BodyDir()
    {
        if(moveDir.x > 0)
        {
            spriteRenderer.sprite = sRender;
            spriteRenderer.flipX = true;
        }
        else if(moveDir.x < 0)
        {
            spriteRenderer.sprite = sRender;
            spriteRenderer.flipX = false;
        }

        if(moveDir.y > 0)
        {
            spriteRenderer.sprite = bRender;
        }
        else if(moveDir.y < 0)
        {
            spriteRenderer.sprite = fRender;
        }
    }

    void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.y = value.Get<Vector2>().y;
    }

    void OnJump()
    {
        Jump(jumpPower);
    }


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

        if (body.transform.position.y <= shdow.position.y+ groundOffset&&curJumpP <= 0.0f)
        {
            isGround = true;
            jumpCount = 0;
            body.transform.position = new Vector2(body.transform.position.x, shdow.position.y + groundOffset);
        }
    }
}
