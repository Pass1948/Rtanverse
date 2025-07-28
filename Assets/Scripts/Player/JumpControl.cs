using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class JumpControl : MonoBehaviour
{
    [Header("플레이어 Flap 세팅")]
    [SerializeField] float jumpPower;
    [SerializeField] float flapForce;
    [SerializeField] float forwardSpeed;

    private Rigidbody2D rd;
    bool isFlap = false;
    bool isDead = false;
    public bool playerDead  { get => isDead; set => isDead = value; } 

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        MoveFowerd();
    }

    void OnJump()
    {
        Jump(jumpPower);
        isFlap = true;
    }


    void MoveFowerd()
    {
        if (isDead == true) 
        {
            rd.velocity = Vector3.zero;
            return;
        }
        else
        {
            Vector3 vector3 = rd.velocity;
            vector3.x = forwardSpeed;
            if (isFlap == true)
            {
                vector3.y = flapForce;
                isFlap = false;
            }
            rd.velocity = vector3;

            float angle = Mathf.Clamp((rd.velocity.y * 10f), -90, 90);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    void Jump(float jumpPower)
    {
        if (isDead == true) 
            return;
        else
        rd.AddForce(new Vector3(0.0f, jumpPower, 0.0f), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacles") || collision.gameObject.CompareTag("BackGround"))
        {
            if (isDead == true)
                return;
            else
            {
                Debug.Log("죽음");
                isDead = true;
                rd.gravityScale = 0f;
                this.enabled = false;
                GameManager.UI.ClosePopUpUI();
                GameManager.UI.ShowPopUpUI<BirdScoreBoardUI>("Prefabs/UI/BirdScoreBoardUI");
            }
        }
    }
}
