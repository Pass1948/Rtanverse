using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Security.Cryptography;
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

    [Header("플레이어 대시 설정")]
    [SerializeField] float dashSpeed;  //10f
    [SerializeField] float dashDuration; // 1f
    [SerializeField] float dashCoolDown; //1f

    [Header("플레이어 이미지")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite sRender;
    [SerializeField] Sprite bRender;
    [SerializeField] Sprite fRender;
    [SerializeField] float shadowMinSize;
    [SerializeField] float sizeSpeed;

    [Header("플레이어 위치 트랜스폼")]
    [SerializeField] Transform body;
    [SerializeField] Transform shadow;
    [SerializeField] Transform rayPoint;

    [Header("버튼 UI")]
    [SerializeField] GameObject buttenUI;

    [Header("총알 발사 설정")]
    [SerializeField] string bulletPrefabPath = "Prefabs/Bullet"; // 총알 프리팹 경로
    [SerializeField] Transform firePoint; // 발사 위치
    [SerializeField] float bulletSpeed = 10f; // 총알 속도
    [SerializeField] float fireRate = 0.5f; // 발사 간격
    [SerializeField] float bulletLifetime = 3f; // 총알 생존 시간

    //====[Jump]====
    private int jumpCount = 0; // 점프 카운터

    private float curJumpP; // 현재 점프
    private float groundOffset = 0.0f;  // 그림자로부터 위치

    private bool isMinSize = false;
    private bool isGround = true; // 땅 착지 상태

    //====[Dash]====
    bool canDash = true;
    float activeSpeed;
    public float _activeSpeed { get => activeSpeed; set => activeSpeed = value; }

    //====[Shooting]====
    private bool canFire = true;
    private float lastFireTime = 0f;

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
    // InputAction에 연결된 함수이름과 함수이름을 일치시켜야 함(작성법 : On+액션이름)

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

    void OnAttack()
    {
        Fire();
    }

    //=======================[Move]=======================
    void Move()
    {
        moveDir.Normalize();
        BodyDir();
        rd.velocity = moveDir * activeSpeed;  // 플레이어 이동 함수(.velocity
    }

    // 플레이어 방향 전환 및 이미지 변경
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

    //=======================[Shooting]=======================

    void Fire()
    {
        if (!canFire || Time.time - lastFireTime < fireRate) return;

        // 총알 생성 (ResourceManager 사용)
        if (firePoint != null)
        {
            GameObject bullet = GameManager.Resource.Instantiate<GameObject>(bulletPrefabPath, firePoint.position, firePoint.rotation, true);
            
            // 총알 방향 설정 (플레이어가 바라보는 방향)
            Vector2 fireDirection = GetFireDirection();
            
            // 총알에 속도 적용
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                bulletRb.velocity = fireDirection * bulletSpeed;
            }

            // 총알 발사 이벤트 발생
            EventManager.ExecuteEvent(new BulletFiredEvent(firePoint.position, fireDirection, bulletSpeed));

            // 총알 자동 삭제 (ResourceManager 사용)
            GameManager.Resource.Destroy(bullet, bulletLifetime);
        }

        lastFireTime = Time.time;
    }

    Vector2 GetFireDirection()
    {
        // 플레이어가 바라보는 방향에 따라 발사 방향 결정
        if (spriteRenderer.sprite == bRender) // 위쪽을 바라보고 있을 때
        {
            return Vector2.up;
        }
        else if (spriteRenderer.sprite == fRender) // 아래쪽을 바라보고 있을 때
        {
            return Vector2.down;
        }
        else // 좌우 방향
        {
            return spriteRenderer.flipX ? Vector2.right : Vector2.left;
        }
    }
}
