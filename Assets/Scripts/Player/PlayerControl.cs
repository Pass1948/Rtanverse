using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{

    [Header("�÷��̾� �̵��ӵ�")]
    [SerializeField] float speed;

    [Header("�÷��̾� ����")]
    [SerializeField] float jumpPower;
    [SerializeField] float gravity;
    [SerializeField] int maxJumpCount;

    [Header("�÷��̾� ��� ����")]
    [SerializeField] float dashSpeed;  //10f
    [SerializeField] float dashDuration; // 1f
    [SerializeField] float dashCoolDown; //1f

    [Header("�÷��̾� �̹���")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite sRender;
    [SerializeField] Sprite bRender;
    [SerializeField] Sprite fRender;
    [SerializeField] float shadowMinSize;
    [SerializeField] float sizeSpeed;

    [Header("�÷��̾� ��ġ ������Ʈ")]
    [SerializeField] Transform body;
    [SerializeField] Transform shadow;
    [SerializeField] Transform rayPoint;

    [Header("��ư UI")]
    [SerializeField] GameObject buttenUI;


    //====[Jump]====
    private int jumpCount = 0; // ���� ī����

    private float curJumpP; // ���� �Ŀ�
    private float groundOffset = 0.0f;  // �ٴڰ��� ���ؼ�ġ

    private bool isMinSize = false;
    private bool isGround = true; // ���� ���� ����


    //====[Dash]====
    bool canDash = true;
    float activeSpeed;
    public float _activeSpeed { get => activeSpeed; set => activeSpeed = value; }

    //====[All]====
    private Rigidbody2D rd;

    private Vector2 moveDir;

    IInteractable currentTarget;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        activeSpeed = speed;
    }

    private void Start()
    {
        activeSpeed = speed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        JumpHandling();
    }

    //=======================[InputSystem]=======================
    // InputAction�� ��ϵ� �̸��� �Լ��̸��� ������ Ȯ��(�ۼ��� : On+Ŀ����̸�)

    void OnMove(InputValue value)
    {
        moveDir.x = value.Get<Vector2>().x;
        moveDir.y = value.Get<Vector2>().y;
    }

    void OnJump()
    {
        Jump(jumpPower);
    }

    void OnInteract()
    {
        if (buttenUI.activeSelf == false) return;
        buttenUI.gameObject.GetComponent<Button>().onClick.Invoke();

        if(currentTarget == null) return;
        activeSpeed = 0f;
        currentTarget.Interact();
    }

    void OnSprint()
    {
        Dash();
    }


    //=======================[Move]=======================
    void Move()
    {
        moveDir.Normalize();
        BodyDir();
        rd.velocity = moveDir * activeSpeed;  // �÷��̾� �̵� �Լ�(.velocity
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
        if (isGround == true)
            return;

        body.transform.position += new Vector3(0.0f, curJumpP * Time.deltaTime, 0.0f);
        curJumpP -= gravity * Time.deltaTime;

        if (shadow.transform.localScale.x > shadowMinSize && isMinSize == false || shadow.transform.localScale.y > shadowMinSize && isMinSize == false)
            shadow.transform.localScale -= new Vector3(shadowMinSize * Time.deltaTime, shadowMinSize * Time.deltaTime, 0.0f);
        else
        {
            isMinSize = true;
            shadow.transform.localScale += new Vector3(sizeSpeed * Time.deltaTime, sizeSpeed * Time.deltaTime, 0.0f);
        }

        if (body.transform.position.y <= shadow.position.y + groundOffset && curJumpP <= 0.0f)
        {
            shadow.transform.localScale = new Vector3(1f, 1f, 0.0f);
            isMinSize = false;
            curJumpP = 0.0f;
            isGround = true;
            jumpCount = 0;
            body.transform.position = new Vector2(body.transform.position.x, shadow.position.y + groundOffset);
        }
    }

    //=======================[Interaction Trigger]=======================

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            currentTarget = collision.GetComponent<IInteractable>();
            buttenUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            currentTarget = null;
            buttenUI.SetActive(false);
        }
    }
    //=======================[Dash]=======================

    void Dash()
    {
        if (!canDash) { return; }
        StartCoroutine(DashCorutine());
    }

    IEnumerator DashCorutine()
    {
        canDash = false;
        activeSpeed = dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        activeSpeed = speed;
        yield return new WaitForSeconds(dashCoolDown);
        canDash = true;
    }
}
