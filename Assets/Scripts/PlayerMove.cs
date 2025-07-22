using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] Sprite sRender;
    [SerializeField] Sprite bRender;
    [SerializeField] Sprite fRender;


    private Rigidbody2D rd;
    private Vector2 moveDir;
    SpriteRenderer spriteRenderer;


    private Animator animator;


    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void FixedUpdate()
    {
        Move();
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


}
