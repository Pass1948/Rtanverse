using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 총알 발사 이벤트
public class BulletFiredEvent : CommonEventBase
{
    public Vector3 FirePosition { get; private set; }
    public Vector2 FireDirection { get; private set; }
    public float BulletSpeed { get; private set; }

    public BulletFiredEvent(Vector3 position, Vector2 direction, float speed)
    {
        FirePosition = position;
        FireDirection = direction;
        BulletSpeed = speed;
    }
}

// 총알 충돌 이벤트
public class BulletHitEvent : CommonEventBase
{
    public GameObject Bullet { get; private set; }
    public GameObject Target { get; private set; }
    public float Damage { get; private set; }
    public Vector2 HitPoint { get; private set; }

    public BulletHitEvent(GameObject bullet, GameObject target, float damage, Vector2 hitPoint)
    {
        Bullet = bullet;
        Target = target;
        Damage = damage;
        HitPoint = hitPoint;
    }
}

// 총알 파괴 이벤트
public class BulletDestroyedEvent : CommonEventBase
{
    public GameObject Bullet { get; private set; }
    public string DestroyReason { get; private set; } // "Hit", "Timeout", "OutOfBounds"

    public BulletDestroyedEvent(GameObject bullet, string reason)
    {
        Bullet = bullet;
        DestroyReason = reason;
    }
}

// 데미지 받음 이벤트
public class DamageTakenEvent : CommonEventBase
{
    public GameObject Target { get; private set; }
    public float Damage { get; private set; }
    public float RemainingHealth { get; private set; }
    public bool IsDead { get; private set; }

    public DamageTakenEvent(GameObject target, float damage, float remainingHealth, bool isDead)
    {
        Target = target;
        Damage = damage;
        RemainingHealth = remainingHealth;
        IsDead = isDead;
    }
} 