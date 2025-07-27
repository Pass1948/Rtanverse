using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockAtRepier : MonoBehaviour
{
    [SerializeField] bool isLookAt = true;

    [SerializeField] float angle =135f;

    private Vector2 mouseDir;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!isLookAt) return;
        LookTarget();
    }

    void LookTarget()
    {
        mouseDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 aimDir = (mouseDir - rb.position).normalized;
        float aimAngle = Mathf.Atan2(aimDir.y, aimDir.x) * Mathf.Rad2Deg - angle;
        transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
    }
}
