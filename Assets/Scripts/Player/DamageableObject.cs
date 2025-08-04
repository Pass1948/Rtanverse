using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [Header("체력 설정")]
    [SerializeField] float maxHealth = 100f;
    [SerializeField] float currentHealth;

    [Header("시각적 효과")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color damageColor = Color.red;
    [SerializeField] float damageFlashDuration = 0.1f;

    private Color originalColor;

    private void Start()
    {
        currentHealth = maxHealth;
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        
        // 데미지 받았을 때 시각적 효과
        if (spriteRenderer != null)
        {
            StartCoroutine(DamageFlash());
        }

        // 체력이 0 이하가 되면 파괴
        if (currentHealth <= 0)
        {
            Die();
        }

        Debug.Log($"{gameObject.name}이(가) {damage} 데미지를 받았습니다. 남은 체력: {currentHealth}");
    }

    private IEnumerator DamageFlash()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageFlashDuration);
        spriteRenderer.color = originalColor;
    }

    private void Die()
    {
        Debug.Log($"{gameObject.name}이(가) 파괴되었습니다.");
        GameManager.Resource.Destroy(gameObject);
    }

    // 체력 회복 메서드
    public void Heal(float healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        Debug.Log($"{gameObject.name}이(가) {healAmount} 체력을 회복했습니다. 현재 체력: {currentHealth}");
    }

    // 현재 체력 비율 반환 (0~1)
    public float GetHealthRatio()
    {
        return currentHealth / maxHealth;
    }
} 